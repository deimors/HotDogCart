using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CustomersTestFixture : ObserverTestFixture<CustomersEvent>
	{
		private Customers _customers;

		[SetUp]
		public override void Setup()
		{
			_customers = new Customers();

			base.Setup();
		}

		protected override IObservable<CustomersEvent> Observable => _customers.Events;

		protected void Act_AddWaitingCustomer()
			=> _customers.AddWaitingCustomer();
	}

	public class CustomerWaitingTests : CustomersTestFixture
	{
		[Test]
		public void AddWaitingCustomer()
		{
			Act_AddWaitingCustomer();

			Assert_EventObserved(new CustomerStartedWaitingEvent());
		}

		[Test]
		public void AddWaitingCustomerTwice()
		{
			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new CustomerStartedWaitingEvent(),
				new PotentialCustomerWalkedAwayEvent()
			);
		}
	}
}