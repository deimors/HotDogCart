using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public abstract class CustomersTestFixture : ObserverTestFixture<CustomersEvent>
	{
		private Customers _customers;
		protected abstract int LineLength { get; }

		[SetUp]
		public override void Setup()
		{
			_customers = new Customers(LineLength);

			base.Setup();
		}

		protected override IObservable<CustomersEvent> Observable => _customers.Events;

		protected void Arrange_SaleStarted()
			=> _customers.CartObserver.OnNext(new SaleStartedEvent());

		protected void Act_AddWaitingCustomer()
			=> _customers.AddWaitingCustomer();
	}

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
				new NoWaitingCustomerEvent()
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
				new NoWaitingCustomerEvent()
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