using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using Zenject;

namespace Assets.Code.Infrastructure
{
	internal class CartToGrillBinding : IInitializable
	{
		private readonly HotDogCart _cart;
		private readonly Grill _grill;

		public CartToGrillBinding(HotDogCart cart, Grill grill)
		{
			_cart = cart;
			_grill = grill;
		}

		public void Initialize()
		{
			_grill.Events.Subscribe(_cart.GrillObserver);
			_cart.Events.OfType<HotDogCartEvent, SaleStartedEvent>().Subscribe(_ => _grill.RemoveCookedHotDog());
		}
	}
}