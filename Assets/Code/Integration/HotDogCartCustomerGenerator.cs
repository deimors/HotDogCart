using System;
using Assets.Code.Model.Selling;
using UniRx;
using Zenject;

namespace Assets.Code.Integration
{
	public class HotDogCartCustomerGenerator
	{
		private static readonly TimeSpan SampleRate = TimeSpan.FromSeconds(2);
		private static readonly float Probability = 0.4f;

		[Inject]
		public void Initialize(Customers customers)
			=> Observable.Timer(SampleRate, SampleRate)
				.Select(_ => UnityEngine.Random.Range(0f, 1f))
				.Where(randomValue => randomValue < Probability)
				.Subscribe(_ => customers.AddWaitingCustomer());
	}
}