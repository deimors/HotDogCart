using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using Zenject;

namespace Assets.Code.Infrastructure
{
	internal class GrillBindings : IInitializable
	{
		private readonly Grill _grill;

		private readonly Time _time;
		private readonly HotDogCart _cart;

		public GrillBindings(Grill grill, Time time, HotDogCart cart)
		{
			_grill = grill;
			_time = time;
			_cart = cart;
		}
		
		public void Initialize()
		{
			_time.Events.Subscribe(_grill.TimeObserver);
			_cart.Events.OfType<HotDogCartEvent, SaleStartedEvent>().Subscribe(_ => _grill.RemoveCookedHotDog());
		}
	}
}