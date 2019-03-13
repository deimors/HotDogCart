using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class SellHotDogTests : HotDogCartTestFixture
	{
		[Test]
		public void SellAndDontProgressTime()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Assert_EventObserved(new SaleStartedEvent());
			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinute()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new TimeProgressedEvent(duration),
				new HotDogSoldEvent()
			);
		}
		
		[Test]
		public void DontSellAndProgressTimeOneMinute()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventObserved(new TimeProgressedEvent(duration));
			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinuteTwice()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogSoldEvent(), 1);
		}

		[Test]
		public void SellAndProgressTimeHalfAMinute()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellWhenAlreadySelling()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeHalfAMinuteTwice()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			var halfMinute = TimeSpan.FromSeconds(30);

			Act_ProgressTime(halfMinute);
			Act_ProgressTime(halfMinute);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new SaleProgressedEvent(0.5f),
				new SaleProgressedEvent(1.0f),
				new HotDogSoldEvent()
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
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Arrange_CustomersEvent(new NoWaitingCustomerEvent());

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new HotDogSoldEvent(), 1);
		}

		[Test]
		public void SellToWaitingCustomerTwice()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new HotDogSoldEvent(),
				new SaleStartedEvent(),
				new HotDogSoldEvent()
			);
		}

		[Test]
		public void SellToWaitingCustomerTwiceWhenSecondCustomerWaitingDuringSell()
		{
			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_Sell();

			Arrange_CustomersEvent(new CustomerStartedWaitingEvent());

			Act_ProgressTime(TimeSpan.FromMinutes(1));
			
			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new HotDogSoldEvent(),
				new SaleStartedEvent(),
				new HotDogSoldEvent()
			);
		}
	}
}
