using System;
using Assets.Code.Model.Selling;
using UniRx;
using UnityEngine;
using Zenject;

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
			=> Observable.EveryUpdate()
				.Select(_ => Time.deltaTime * TimeScale)
				.Select(deltaTime => TimeSpan.FromSeconds(deltaTime))
				.Subscribe(ProgressTime);

		private void ProgressTime(TimeSpan span)
		{
			Cart.ProgressTime(span);
			Grill.ProgressTime(span);
		}
	}
}