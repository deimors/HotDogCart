using System;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class HotDogCartTestFixture
	{
		private readonly HotDogCart _pos;
		private readonly IObserver<HotDogCartEvent> _observer = Substitute.For<IObserver<HotDogCartEvent>>();

		public HotDogCartTestFixture(TimeSpan sellTime)
		{
			_pos = new HotDogCart(sellTime);
			_pos.Events.Subscribe(_observer);
		}

		protected void Act_Sell()
			=> _pos.Sell();

		protected void Act_Wait(TimeSpan duration)
			=> _pos.Wait(duration);

		protected void Assert_EventObserved(HotDogCartEvent expected)
			=> _observer.Received().OnNext(expected);

		protected void Assert_EventNotObserved(HotDogCartEvent prohibited)
			=> _observer.DidNotReceive().OnNext(prohibited);
	}

	public class HotDogCartTests : HotDogCartTestFixture
	{
		public HotDogCartTests() : base(sellTime: TimeSpan.FromMinutes(1))
		{
		}

		[Test]
		public void SellHotDogInABunAndDontWait()
		{
			Act_Sell();

			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellHotDogInABunAndWaitOneMinute()
		{
			Act_Sell();

			Act_Wait(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogInABunSoldEvent());
		}
	}
}
