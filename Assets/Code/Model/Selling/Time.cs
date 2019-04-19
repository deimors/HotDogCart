using Assets.Code.Model.Selling.Events;
using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Time
	{
		private readonly ISubject<TimeEvent> _events = new Subject<TimeEvent>();
		public IObservable<TimeEvent> Events => _events;

		private DateTime _currentTime = DateTime.MinValue;

		public void Progress(TimeSpan duration)
		{
			_currentTime += duration;

			_events.OnNext(new TimeProgressedEvent(duration, _currentTime));
		}
	}
}