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

		private TimeSpan? _remainingCookTime;
		private static readonly TimeSpan CookTime = TimeSpan.FromMinutes(5);

		private int _nextIndex;

		public void AddHotDog()
		{
			_remainingCookTime = CookTime;

			_events.OnNext(new HotDogAddedEvent(_nextIndex++));
		}

		public void ProgressTime(TimeSpan duration)
		{
			_remainingCookTime -= duration;

			var progress = 1 - ((double)(_remainingCookTime?.Ticks ?? 0) / CookTime.Ticks);

			foreach (var index in Enumerable.Range(0, _nextIndex))
			{
				_events.OnNext(new CookingProgressedEvent(index, (float)progress));

				if (progress >= 1)
					_events.OnNext(new HotDogCookedEvent(index));
			}
		}	
	}
}