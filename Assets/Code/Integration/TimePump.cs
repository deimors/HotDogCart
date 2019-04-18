using System;
using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using UnityEngine;
using Zenject;
using Time = UnityEngine.Time;

namespace Assets.Code.Integration
{
	public class TimePump
	{
		private const float TimeScale = 30;

		[Inject]
		public HotDogCart Cart { private get; set; }

		[Inject]
		public Grill Grill { private get; set; }

		[Inject]
		public void Initialize()
		{
			var timeEvents = Observable.EveryUpdate()
				.Select(_ => Time.deltaTime * TimeScale)
				.Select(deltaTime => TimeSpan.FromSeconds(deltaTime))
				.Select(duration => new TimeProgressedEvent(duration) as TimeEvent);

			timeEvents.Subscribe(Cart.TimeObserver);
			timeEvents.Subscribe(Grill.TimeObserver);
		}
	}
}