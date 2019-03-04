using System;
using Assets.Code.Model.Selling;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.Infrastructure
{
	public class HotDogCartCustomerGenerator
	{
		[Inject]
		public void Initialize(HotDogCart cart)
			=> Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
				.Select(_ => UnityEngine.Random.Range(0f, 1f))
				.Where(randomValue => randomValue < 0.2f)
				.Subscribe(_ => cart.AddWaitingCustomer());
	}
}