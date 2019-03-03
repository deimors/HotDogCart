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
}