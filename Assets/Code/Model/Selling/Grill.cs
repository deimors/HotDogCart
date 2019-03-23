using System;
using System.Linq;
using Assets.Code.Model.Selling.Events;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Grill
	{
		private readonly ISubject<GrillEvent> _events = new Subject<GrillEvent>();
		public IObservable<GrillEvent> Events => _events;

		private readonly TimeSpan?[] _remainingCookTimes = new TimeSpan?[2];

		private static readonly TimeSpan CookTime = TimeSpan.FromMinutes(5);

		public void AddHotDog()
		{
			var addIndex = IndexOfFirstEmptyRemainingTime;

			if (addIndex.HasValue)
			{
				_remainingCookTimes[addIndex.Value] = CookTime;

				_events.OnNext(new HotDogAddedEvent(addIndex.Value));
			}
		}
		
		public void ProgressTime(TimeSpan duration)
		{
			foreach (var index in Enumerable.Range(0, _remainingCookTimes.Length))
			{
				if (!HasStartedCooking(index) || HasCompletedCooking(index)) continue;

				DecreaseRemainingCookTime(index, duration);

				var progress = GetProgress(index);

				_events.OnNext(new CookingProgressedEvent(index, (float)progress));

				if (progress >= 1)
					_events.OnNext(new HotDogCookedEvent(index));
			}
		}

		public void RemoveCookedHotDog()
		{
			var removeIndex = IndexOfFirstRemainingTime;

			if (removeIndex.HasValue && HasCompletedCooking(removeIndex.Value))
			{
				_remainingCookTimes[removeIndex.Value] = null;
				_events.OnNext(new CookedHotDogRemovedEvent(removeIndex.Value));
			}
		}

		private int? IndexOfFirstRemainingTime 
			=> _remainingCookTimes
				.Select((time, index) => new {time, index})
				.FirstOrDefault(anon => anon.time.HasValue)
				?.index;

		private int? IndexOfFirstEmptyRemainingTime 
			=> _remainingCookTimes
				.Select((time, index) => new { time, index })
				.FirstOrDefault(anon => !anon.time.HasValue)
				?.index;

		private double GetProgress(int index)
			=> 1 - ((double)(_remainingCookTimes[index]?.Ticks ?? 0) / CookTime.Ticks);

		private void DecreaseRemainingCookTime(int index, TimeSpan duration)
			=> _remainingCookTimes[index] -= 
				duration > _remainingCookTimes[index] 
					? _remainingCookTimes[index] 
					: duration;

		private bool HasCompletedCooking(int index)
			=> _remainingCookTimes[index] == TimeSpan.Zero;

		private bool HasStartedCooking(int index)
			=> _remainingCookTimes[index].HasValue;	
	}
}