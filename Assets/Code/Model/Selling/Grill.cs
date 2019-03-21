﻿using System;
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

		public void AddHotDog()
		{
			_remainingCookTime = CookTime;

			_events.OnNext(new HotDogAddedEvent(0));
		}

		public void ProgressTime(TimeSpan duration)
		{
			_remainingCookTime -= duration;

			var progress = 1 - ((double)(_remainingCookTime?.Ticks ?? 0) / CookTime.Ticks);

			_events.OnNext(new CookingProgressedEvent((float) progress));

			if (progress >= 1)
				_events.OnNext(new HotDogCookedEvent(0));
		}	
	}
}