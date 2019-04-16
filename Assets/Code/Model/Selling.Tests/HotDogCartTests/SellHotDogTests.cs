using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.HotDogCartTests
{
	public class SellHotDogTests : HotDogCartTestFixture
	{
		private void Arrange_CustomerAndHotDogAvailable()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());
			Arrange_GrillEvent(new CookedHotDogsAvailableEvent());
		}

		[Test]
		public void SellAndDontProgressTime()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Assert_EventObserved(new SaleStartedEvent());
			Assert_EventNotObserved(new SaleCompletedEvent());
		}
		
		[Test]
		public void SellAvailableHotDogToWaitingCustomerAndProgressTimeFull()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(SellTime);

			Assert_EventsObserved(
				new CanSellEvent(),
				new CantSellEvent(),
				new SaleStartedEvent(),
				new CanSellEvent(),
				new SaleCompletedEvent()
			);
		}

		[Test]
		public void SellWhenNoCookedHotDogAvailable()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Assert_EventNotObserved(Arg.Any<CanSellEvent>());
			Assert_EventNotObserved(Arg.Any<SaleStartedEvent>());
		}

		[Test]
		public void DontSellAndProgressTimeOneMinute()
		{
			Arrange_CustomerAndHotDogAvailable();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventNotObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinuteTwice()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new SaleCompletedEvent(), 1);
		}

		[Test]
		public void SellAndProgressTimeHalfAMinute()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellWhenAlreadySelling()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromSeconds(30));

			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellAndProgressTimeHalfAMinuteTwice()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			var halfMinute = TimeSpan.FromSeconds(30);

			Arrange_TimeProgressed(halfMinute);
			Arrange_TimeProgressed(halfMinute);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new SaleProgressedEvent(0.5f),
				new SaleProgressedEvent(1.0f),
				new SaleCompletedEvent()
			);
		}

		[Test]
		public void SellWithoutWaitingCustomer()
		{
			Act_Sell();

			Assert_EventNotObserved(new SaleStartedEvent());
		}

		[Test]
		public void SellToWaitingCustomerThenTryToSellAgain()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new SaleCompletedEvent(), 1);
		}

		[Test]
		public void SellToWaitingCustomerTwice()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new SaleCompletedEvent(),
				new SaleStartedEvent(),
				new SaleCompletedEvent()
			);
		}

		[Test]
		public void SellToWaitingCustomerTwiceWhenSecondCustomerWaitingDuringSell()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_CustomerAndHotDogAvailable();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));
			
			Act_Sell();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new SaleCompletedEvent(),
				new SaleStartedEvent(),
				new SaleCompletedEvent()
			);
		}

		[Test]
		public void CustomerArrivesDuringSale()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Arrange_CustomerAndHotDogAvailable();
			
			Assert_EventObserved(new CanSellEvent(), 1);
		}

		[Test]
		public void CustomerArrivesDuringSaleThenCompleteSale()
		{
			Arrange_CustomerAndHotDogAvailable();

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Arrange_CustomerAndHotDogAvailable();

			Arrange_TimeProgressed(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new CanSellEvent(),
				new CantSellEvent(),
				new SaleStartedEvent(),
				new CanSellEvent(),
				new SaleCompletedEvent()
			);
		}
	}
}
