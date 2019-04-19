using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Model.Selling.Events;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Grill
	{
		private readonly ISubject<GrillEvent> _events = new Subject<GrillEvent>();
		private readonly ISubject<TimeEvent> _timeEvents = new Subject<TimeEvent>();
		
		private readonly TimeSpan?[] _cookingSlots;

		private static readonly TimeSpan CookTime = TimeSpan.FromMinutes(5);

		public Grill(int slotCount)
		{
			_cookingSlots = new TimeSpan?[slotCount];

			_timeEvents
				.OfType<TimeEvent, TimeProgressedEvent>()
				.Select(e => e.Duration)
				.Subscribe(ProgressTime);
		}

		public IObservable<GrillEvent> Events => _events;

		public IObserver<TimeEvent> TimeObserver => _timeEvents;

		public void AddHotDog()
		{
			var addIndex = IndexOfFirstEmptySlot;

			if (addIndex.HasValue)
			{
				_cookingSlots[addIndex.Value] = CookTime;

				_events.OnNext(new HotDogAddedEvent(addIndex.Value));

				if (EmptySlotCount == 0)
					_events.OnNext(new CantAddHotDogEvent());
			}
		}
		
		public void RemoveCookedHotDog()
		{
			var removeIndex = IndexOfFirstCookedSlot;
			
			if (removeIndex.HasValue)
			{
				_cookingSlots[removeIndex.Value] = null;
				_events.OnNext(new CookedHotDogRemovedEvent(removeIndex.Value));

				if (EmptySlotCount == 1)
					_events.OnNext(new CanAddHotDogEvent());

				if (CookedHotDogCount == 0)
					_events.OnNext(new NoCookedHotDogsAvailableEvent());
			}
		}

		private void ProgressTime(TimeSpan duration)
		{
			foreach (var index in IndicesOfSlotsCurrentlyCooking)
				ProgressCooking(duration, index);
		}

		private IEnumerable<int> IndicesOfSlotsCurrentlyCooking
			=> SlotIndices.Where(index => HasStartedCooking(index) && !HasCompletedCooking(index));

		private IEnumerable<int> SlotIndices 
			=> Enumerable.Range(0, _cookingSlots.Length);

		private int? IndexOfFirstCookedSlot 
			=> _cookingSlots
				.Select((time, index) => new {time, index})
				.FirstOrDefault(anon => anon.time.HasValue && HasCompletedCooking(anon.index))
				?.index;

		private int? IndexOfFirstEmptySlot 
			=> _cookingSlots
				.Select((time, index) => new { time, index })
				.FirstOrDefault(anon => !anon.time.HasValue)
				?.index;

		private int CookedHotDogCount
			=> SlotIndices.Count(HasCompletedCooking);

		private int EmptySlotCount
			=> SlotIndices.Count(index => !HasStartedCooking(index));

		private void ProgressCooking(TimeSpan duration, int index)
		{
			DecreaseRemainingCookTime(index, duration);

			_events.OnNext(new CookingProgressedEvent(index, (float)GetProgress(index)));

			if (HasCompletedCooking(index))
			{
				_events.OnNext(new HotDogCookedEvent(index));

				if (CookedHotDogCount == 1)
					_events.OnNext(new CookedHotDogsAvailableEvent());
			}
		}

		private double GetProgress(int index)
			=> 1 - ((double)(_cookingSlots[index]?.Ticks ?? 0) / CookTime.Ticks);

		private void DecreaseRemainingCookTime(int index, TimeSpan duration)
			=> _cookingSlots[index] -= 
				duration > _cookingSlots[index] 
					? _cookingSlots[index] 
					: duration;

		private bool HasCompletedCooking(int index)
			=> _cookingSlots[index] == TimeSpan.Zero;

		private bool HasStartedCooking(int index)
			=> _cookingSlots[index].HasValue;	
	}
}