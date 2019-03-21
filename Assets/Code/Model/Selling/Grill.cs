using System;
using Assets.Code.Model.Selling.Events;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Grill
	{
		private readonly ISubject<GrillEvent> _events = new Subject<GrillEvent>();
		public IObservable<GrillEvent> Events => _events;

		public void ProgressTime(TimeSpan duration)
		{
			
		}

		public void AddHotDog()
		{
			_events.OnNext(new HotDogAddedEvent(0));
			_events.OnNext(new CookingProgressedEvent(1.0f));
			_events.OnNext(new HotDogCookedEvent(0));
		}
	}
}