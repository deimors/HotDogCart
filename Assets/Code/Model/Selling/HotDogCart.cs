using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly TimeSpan _sellTime;
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();

		private TimeSpan? _remainingTime;

		public HotDogCart(TimeSpan sellTime)
		{
			_sellTime = sellTime;
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			_remainingTime = _sellTime;
		}

		public void Wait(TimeSpan duration)
		{
			if (!_remainingTime.HasValue)
				return;

			_remainingTime -= duration;

			if (_remainingTime > TimeSpan.Zero)
				return;

			_remainingTime = null;
			
			_events.OnNext(new HotDogInABunSoldEvent());
		}
	}
}
