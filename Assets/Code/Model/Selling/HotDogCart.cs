using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();
		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			
		}
	}
}
