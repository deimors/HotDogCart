using Assets.Code.Model.Selling;
using Zenject;

namespace Assets.Code.Infrastructure
{
	internal class CustomersBinding : IInitializable
	{
		private readonly HotDogCart _cart;
		private readonly Customers _customers;

		public CustomersBinding(HotDogCart cart, Customers customers)
		{
			_cart = cart;
			_customers = customers;
		}

		public void Initialize()
		{
			_cart.Events.Subscribe(_customers.CartObserver);
		}
	}
}