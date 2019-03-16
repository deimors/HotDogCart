using System;
using Assets.Code.Model.Selling;
using UniRx;
using Zenject;

namespace Assets.Code.Integration
{
	public class HotDogCartCustomerGenerator
	{
		[Inject]
		public void Initialize(Customers customers)
			=> Observable.Timer(TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.5))
				.Select(_ => UnityEngine.Random.Range(0f, 1f))
				.Where(randomValue => randomValue < 0.2f)
				.Subscribe(_ => customers.AddWaitingCustomer());
	}
}