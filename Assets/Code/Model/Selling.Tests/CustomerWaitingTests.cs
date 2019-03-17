using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CustomerWaitingTests : CustomersTestFixture
	{
		protected override int MaxLineLength => 2;

		[Test]
		public void AddWaitingCustomer()
		{
			Act_AddWaitingCustomer();

			Assert_EventObserved(new CustomersWaitingEvent());
		}

		[Test]
		public void LineFull()
		{
			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new CustomersWaitingEvent(),
				new CustomersWaitingEvent(),
				new MissedCustomerEvent()
			);
		}

		[Test]
		public void OneWaitingCustomerServed()
		{
			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Assert_EventsObserved(
				new CustomersWaitingEvent(),
				new NoWaitingCustomersEvent()
			);
		}

		[Test]
		public void TwoWaitingCustomersServed()
		{
			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Arrange_SaleStarted();

			Assert_EventsObserved(
				new CustomersWaitingEvent(),
				new CustomersWaitingEvent(),
				new NoWaitingCustomersEvent()
			);
		}

		[Test]
		public void SaleStartedBetweenTwoAddWaitingCustomers()
		{
			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new CustomersWaitingEvent(),
				new CustomersWaitingEvent()
			);
		}
	}
}