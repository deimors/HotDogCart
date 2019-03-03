using System;
using Assets.Code.Model.Selling;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.Infrastructure
{
	public class HotDogCartTimePump
	{
		private const float TimeScale = 30;

		[Inject]
		public void Initialize(HotDogCart cart)
			=> Observable.EveryUpdate()
				.Select(_ => Time.deltaTime * TimeScale)
				.Select(deltaTime => TimeSpan.FromSeconds(deltaTime))
				.Subscribe(cart.ProgressTime);
	}
}