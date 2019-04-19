using Assets.Code.Model.Selling.Events;
using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Time
	{
		private readonly ISubject<TimeEvent> _events = new Subject<TimeEvent>();
		public IObservable<TimeEvent> Events => _events;

		public void Progress(TimeSpan duration)
		{
			_events.OnNext(new TimeProgressedEvent(duration, default(DateTime)));
		}
	}
}