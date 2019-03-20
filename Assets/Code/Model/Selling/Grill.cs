using System;
using Assets.Code.Model.Selling.Events;

namespace Assets.Code.Model.Selling
{
	public class Grill
	{
		public IObservable<GrillEvent> Events { get; }

		public void ProgressTime(TimeSpan duration)
		{
			
		}

		public void AddHotDog()
		{
			
		}
	}
}