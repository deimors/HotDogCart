using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CustomerWaitingTests : HotDogCartTestFixture
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
				new CustomerWalkedAwayEvent()
			);
		}
	}
}