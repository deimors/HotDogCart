using System;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class HotDogCartTestFixture
	{
		private readonly HotDogCart _pos = new HotDogCart();
		private readonly IObserver<HotDogCartEvent> _observer = Substitute.For<IObserver<HotDogCartEvent>>();

		public HotDogCartTestFixture()
		{
			_pos.Events.Subscribe(_observer);
		}

		protected void Act_Sell()
			=> _pos.Sell();

		protected void Assert_EventObserved(HotDogCartEvent expected)
			=> _observer.Received().OnNext(expected);
	}

	public class HotDogCartTests : HotDogCartTestFixture
	{
		[Test]
		public void SellHotDogInABun()
		{
			Act_Sell();

			Assert_EventObserved(new HotDogInABunSoldEvent());
		}
	}
}
