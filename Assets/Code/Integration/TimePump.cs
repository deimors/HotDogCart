using System;
using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using Zenject;

namespace Assets.Code.Integration
{
	public class TimePump
	{
		private const float TimeScale = 30;

		[Inject]
		public Time Time { private get; set; }

		[Inject]
		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => UnityEngine.Time.deltaTime * TimeScale)
				.Select(deltaTime => TimeSpan.FromSeconds(deltaTime))
				.Subscribe(Time.Progress);
		}
	}
}