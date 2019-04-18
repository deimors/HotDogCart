using System;
using Assets.Code.Model.Selling.Events;

namespace Assets.Code.Model.Selling
{
	public class Time
	{
		public IObservable<TimeEvent> Events { get; }

		public void Progress(TimeSpan duration)
		{
			
		}
	}
}