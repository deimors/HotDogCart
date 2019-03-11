using Assets.Code.Model.Selling;
using Zenject;

namespace Assets.Code.Infrastructure
{
	internal class CartToCustomersBinding : IInitializable
	{
		private readonly HotDogCart _cart;
		private readonly Customers _customers;

		public CartToCustomersBinding(HotDogCart cart, Customers customers)
		{
			_cart = cart;
			_customers = customers;
		}

		public void Initialize()
		{
			_cart.Events.Subscribe(_customers.CartObserver);
			_customers.Events.Subscribe(_cart.CustomersObserver);
		}
	}
}