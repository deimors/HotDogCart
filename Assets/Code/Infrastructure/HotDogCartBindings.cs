using Assets.Code.Model.Selling;
using Zenject;

namespace Assets.Code.Infrastructure
{
	internal class HotDogCartBindings : IInitializable
	{
		private readonly HotDogCart _cart;

		private readonly Grill _grill;
		private readonly Customers _customers;
		private readonly Time _time;

		public HotDogCartBindings(HotDogCart cart, Grill grill, Customers customers, Time time)
		{
			_cart = cart;
			_grill = grill;
			_customers = customers;
			_time = time;
		}

		public void Initialize()
		{
			_grill.Events.Subscribe(_cart.GrillObserver);
			_customers.Events.Subscribe(_cart.CustomersObserver);
			_time.Events.Subscribe(_cart.TimeObserver);
		}
	}
}