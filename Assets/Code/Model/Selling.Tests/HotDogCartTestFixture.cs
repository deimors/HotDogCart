using System;
using NSubstitute;
using NUnit.Framework;
using UniRx;

namespace Assets.Code.Model.Selling.Tests
{
	public class HotDogCartTestFixture
	{
		private HotDogCart _pos;
		private IObserver<HotDogCartEvent> _observer;
		private ISubject<CustomersEvent> _customersEvents;

		protected TimeSpan SellTime { get; set; } = TimeSpan.FromMinutes(1);
		
		[SetUp]
		public void Setup()
		{
			_customersEvents = new Subject<CustomersEvent>();

			_pos = new HotDogCart(_customersEvents, SellTime);

			_observer = Substitute.For<IObserver<HotDogCartEvent>>();
			_pos.Events.Subscribe(_observer);
		}

		protected void Arrange_CustomerStartedWaiting()
			=> _customersEvents.OnNext(new CustomerStartedWaitingEvent());

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