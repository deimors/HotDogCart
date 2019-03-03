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
	}
}