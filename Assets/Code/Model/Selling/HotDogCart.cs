using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly TimeSpan _sellTime;
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();

		private TimeSpan? _remainingSaleTime;

		public HotDogCart(TimeSpan sellTime)
		{
			_sellTime = sellTime;
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			if (!IsSaleActive)
				StartSale();
		}
		
		public void ProgressTime(TimeSpan duration)
		{
			_events.OnNext(new TimeProgressedEvent(TimeSpan.FromMinutes(1)));

			if (!IsSaleActive)
				return;

			ReduceRemainingSaleTime(duration);

			if (IsTimeRemainingInSale)
				return;

			CompleteSale();
			
			_events.OnNext(new HotDogInABunSoldEvent());
		}
		
		private bool IsSaleActive => _remainingSaleTime.HasValue;

		private bool IsTimeRemainingInSale => _remainingSaleTime > TimeSpan.Zero;

		private void StartSale()
			=> _remainingSaleTime = _sellTime;

		private void CompleteSale()
			=> _remainingSaleTime = null;

		private void ReduceRemainingSaleTime(TimeSpan duration)
			=> _remainingSaleTime -= duration;
	}
}
