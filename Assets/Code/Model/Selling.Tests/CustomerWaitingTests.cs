using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CustomerWaitingTests : CustomersTestFixture
	{
		protected override int LineLength => 2;

		[Test]
		public void AddWaitingCustomer()
		{
			Act_AddWaitingCustomer();

			Assert_EventObserved(new CustomerStartedWaitingEvent());
		}

		[Test]
		public void LineFull()
		{
			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new CustomerStartedWaitingEvent(),
				new CustomerStartedWaitingEvent(),
				new MissedCustomerEvent()
			);
		}

		[Test]
		public void OneWaitingCustomerServed()
		{
			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Assert_EventsObserved(
				new CustomerStartedWaitingEvent(),
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
				new CustomerStartedWaitingEvent(),
				new CustomerStartedWaitingEvent(),
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
				new CustomerStartedWaitingEvent(),
				new CustomerStartedWaitingEvent()
			);
		}
	}
}