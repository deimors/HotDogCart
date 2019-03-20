using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class SellHotDogTests : HotDogCartTestFixture
	{
		[Test]
		public void SellAndDontProgressTime()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Assert_EventObserved(new SaleStartedEvent());
			Assert_EventNotObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinute()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventsObserved(
				new CanSellEvent(),
				new SaleStartedEvent(),
				new CantSellEvent(),
				new TimeProgressedEvent(duration),
				new SaleCompletedEvent(),
				new CanSellEvent()
			);
		}
		
		[Test]
		public void DontSellAndProgressTimeOneMinute()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventObserved(new TimeProgressedEvent(duration));
			Assert_EventNotObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinuteTwice()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new SaleCompletedEvent(), 1);
		}

		[Test]
		public void SellAndProgressTimeHalfAMinute()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellWhenAlreadySelling()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new SaleCompletedEvent());
		}

		[Test]
		public void SellAndProgressTimeHalfAMinuteTwice()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			var halfMinute = TimeSpan.FromSeconds(30);

			Act_ProgressTime(halfMinute);
			Act_ProgressTime(halfMinute);

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
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new SaleCompletedEvent(), 1);
		}

		[Test]
		public void SellToWaitingCustomerTwice()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

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
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_ProgressTime(TimeSpan.FromMinutes(1));
			
			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

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
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Arrange_CustomersEvent(new LineNotEmptyEvent());
			
			Assert_EventObserved(new CanSellEvent(), 1);
		}

		[Test]
		public void CustomerArrivesDuringSaleThenCompleteSale()
		{
			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_Sell();

			Arrange_CustomersEvent(new LineEmptyEvent());

			Arrange_CustomersEvent(new LineNotEmptyEvent());

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new CanSellEvent(),
				new SaleStartedEvent(),
				new CantSellEvent(),
				new SaleCompletedEvent(),
				new CanSellEvent()
			);
		}
	}
}
