using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly IObservable<CustomersEvent> _customersEvents;
		private readonly TimeSpan _sellTime;
		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();

		private TimeSpan? _remainingSaleTime;
		private bool _customerWaiting;

		public HotDogCart(IObservable<CustomersEvent> customersEvents, TimeSpan sellTime)
		{
			_customersEvents = customersEvents;
			_sellTime = sellTime;

			_customersEvents
				.OfType<CustomersEvent, CustomerStartedWaitingEvent>()
				.Subscribe(_ => _customerWaiting = true);
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public void Sell()
		{
			if (IsSaleActive || !_customerWaiting)
				return;

			StartSale();
			_events.OnNext(new SaleStartedEvent());
		}
		
		public void ProgressTime(TimeSpan duration)
		{
			_events.OnNext(new TimeProgressedEvent(TimeSpan.FromMinutes(1)));
			
			if (!IsSaleActive)
				return;

			ReduceRemainingSaleTime(duration);

			_events.OnNext(new SaleProgressedEvent(Progress));

			if (IsTimeRemainingInSale)
				return;

			CompleteSale();
			
			_events.OnNext(new HotDogSoldEvent());
		}

		private bool IsSaleActive => _remainingSaleTime.HasValue;

		private bool IsTimeRemainingInSale => _remainingSaleTime > TimeSpan.Zero;

		private void StartSale()
		{
			_remainingSaleTime = _sellTime;
			_customerWaiting = false;
		}

		private void CompleteSale()
			=> _remainingSaleTime = null;

		private void ReduceRemainingSaleTime(TimeSpan duration)
			=> _remainingSaleTime -= duration;

		private float Progress
			=> _remainingSaleTime.HasValue 
				? 1f - (float)(_remainingSaleTime.Value.TotalMilliseconds / _sellTime.TotalMilliseconds) 
				: 0;
	}
}
