using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();
		
		public HotDogCart(TimeSpan sellTime)
		{
			
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			
		}

		public void Wait(TimeSpan duration)
		{
			_events.OnNext(new HotDogInABunSoldEvent());
		}
	}
}
