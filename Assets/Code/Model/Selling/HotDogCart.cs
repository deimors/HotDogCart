using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();

		private bool _sold;

		public HotDogCart(TimeSpan sellTime)
		{
			
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			_sold = true;
		}

		public void Wait(TimeSpan duration)
		{
			if (_sold)
				_events.OnNext(new HotDogInABunSoldEvent());
		}
	}
}
