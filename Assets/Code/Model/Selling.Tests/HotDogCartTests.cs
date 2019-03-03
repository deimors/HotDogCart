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

		protected void Act_ProgressTime(TimeSpan duration)
			=> _pos.ProgressTime(duration);

		protected void Assert_EventObserved(HotDogCartEvent expected)
			=> _observer.Received().OnNext(expected);

		protected void Assert_EventObserved(HotDogCartEvent expected, int repetitions)
			=> _observer.Received(repetitions).OnNext(expected);

		protected void Assert_EventNotObserved(HotDogCartEvent prohibited)
			=> _observer.DidNotReceive().OnNext(prohibited);

		protected void Assert_EventsObserved(params HotDogCartEvent[] expected)
			=> Received.InOrder(() =>
			{
				foreach (var expectedEvent in expected)
					_observer.Received().OnNext(expectedEvent);
			});
	}

	public class HotDogCartTests : HotDogCartTestFixture
	{
		[Test]
		public void SellAndDontProgressTime()
		{
			Act_Sell();

			Assert_EventObserved(new SaleStartedEvent());
			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinute()
		{
			Act_Sell();

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new TimeProgressedEvent(duration),
				new HotDogInABunSoldEvent()
			);
		}
		
		[Test]
		public void DontSellAndProgressTimeOneMinute()
		{
			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventObserved(new TimeProgressedEvent(duration));
			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinuteTwice()
		{
			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogInABunSoldEvent(), 1);
		}

		[Test]
		public void SellAndProgressTimeHalfAMinute()
		{
			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new HotDogInABunSoldEvent());
		}

		[Test]
		public void SellWhenAlreadySelling()
		{
			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new HotDogInABunSoldEvent());
		}
	}
}
