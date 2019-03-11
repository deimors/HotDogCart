using NUnit.Framework;
using System;

namespace Assets.Code.Model.Selling.Tests
{
	public class HotDogCartTestFixture : ObserverTestFixture<HotDogCartEvent>
	{
		private HotDogCart _pos;

		protected TimeSpan SellTime { get; set; } = TimeSpan.FromMinutes(1);
		
		[SetUp]
		public override void Setup()
		{
			_pos = new HotDogCart(SellTime);
			
			base.Setup();
		}

		protected override IObservable<HotDogCartEvent> Observable => _pos.Events;

		protected void Arrange_CustomerStartedWaiting()
			=> _pos.CustomersObserver.OnNext(new CustomerStartedWaitingEvent());

		protected void Act_Sell()
			=> _pos.Sell();

		protected void Act_ProgressTime(TimeSpan duration)
			=> _pos.ProgressTime(duration);
	}
}