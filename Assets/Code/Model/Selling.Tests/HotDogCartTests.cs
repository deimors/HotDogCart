using System;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class HotDogCartTestFixture
	{
		private HotDogCart _pos;
		private IObserver<HotDogCartEvent> _observer = Substitute.For<IObserver<HotDogCartEvent>>();

		protected TimeSpan SellTime { get; set; } = TimeSpan.FromMinutes(1);
		
		[SetUp]
		public void Setup()
		{
			_pos = new HotDogCart(SellTime);
			_observer = Substitute.For<IObserver<HotDogCartEvent>>();
			_pos.Events.Subscribe(_observer);
		}

		protected void Act_Sell()
			=> _pos.Sell();

		protected void Act_Wait(TimeSpan duration)
			=> _pos.Wait(duration);

		protected void Assert_EventObserved(HotDogCartEvent expected)
			=> _observer.Received().OnNext(expected);

		protected void Assert_EventObserved(HotDogCartEvent expected, int repetitions)
			=> _observer.Received(repetitions).OnNext(expected);

		protected void Assert_EventNotObserved(HotDogCartEvent prohibited)
			=> _observer.DidNotReceive().OnNext(prohibited);
	}

	public class HotDogCartTests : HotDogCartTestFixture
	{
		[Test]
		public void SellAndDontWait()
		{
			Act_Sell();

			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellAndWaitOneMinute()
		{
			Act_Sell();

			Act_Wait(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogInABunSoldEvent());
		}
		
		[Test]
		public void DontSellAndWaitOneMinute()
		{
			Act_Wait(TimeSpan.FromMinutes(1));

			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellAndWaitOneMinuteTwice()
		{
			Act_Sell();

			Act_Wait(TimeSpan.FromMinutes(1));

			Act_Wait(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogInABunSoldEvent(), 1);
		}

		[Test]
		public void SellAndWaitHalfAMinute()
		{
			Act_Sell();

			Act_Wait(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void TryToSellWhenAlreadySelling()
		{
			Act_Sell();

			Act_Wait(TimeSpan.FromSeconds(30));

			Act_Sell();

			Act_Wait(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new HotDogInABunSoldEvent());
		}
	}
}
