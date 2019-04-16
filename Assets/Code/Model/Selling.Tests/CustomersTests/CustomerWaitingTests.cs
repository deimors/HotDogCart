using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.CustomersTests
{
	public class CustomerWaitingTests : CustomersTestFixture
	{
		protected override int MaxLineLength => 2;

		[Test]
		public void AddWaitingCustomer()
		{
			Act_AddWaitingCustomer();

			Assert_EventObserved(new LineNotEmptyEvent());
		}

		[Test]
		public void LineFull()
		{
			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new LineLengthIncreasedEvent(1),
				new LineNotEmptyEvent(),
				new LineLengthIncreasedEvent(2),
				new MissedCustomerEvent()
			);
		}

		[Test]
		public void OneWaitingCustomerServed()
		{
			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Assert_EventsObserved(
				new LineLengthIncreasedEvent(1),
				new LineNotEmptyEvent(),
				new LineLengthDecreasedEvent(lineLength: 0),
				new LineEmptyEvent()
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
				new LineLengthIncreasedEvent(1),
				new LineNotEmptyEvent(),
				new LineLengthIncreasedEvent(2),
				new LineLengthDecreasedEvent(lineLength: 1),
				new LineLengthDecreasedEvent(lineLength: 0),
				new LineEmptyEvent()
			);
		}

		[Test]
		public void SaleStartedBetweenTwoAddWaitingCustomers()
		{
			Act_AddWaitingCustomer();

			Arrange_SaleStarted();

			Act_AddWaitingCustomer();

			Assert_EventsObserved(
				new LineLengthIncreasedEvent(1),
				new LineNotEmptyEvent(),
				new LineLengthDecreasedEvent(lineLength: 0),
				new LineEmptyEvent(),
				new LineLengthIncreasedEvent(1),
				new LineNotEmptyEvent()
			);
		}
	}
}