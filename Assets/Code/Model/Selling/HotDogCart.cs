﻿using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly TimeSpan _sellTime;

		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();
		private readonly ISubject<CustomersEvent> _customersEvents = new Subject<CustomersEvent>();
		
		private TimeSpan? _remainingSaleTime;
		private bool _customerWaiting;

		public HotDogCart(TimeSpan sellTime)
		{
			_sellTime = sellTime;

			_customersEvents
				.OfType<CustomersEvent, CustomerStartedWaitingEvent>()
				.Subscribe(_ =>
				{
					_customerWaiting = true;

					if (!IsSaleActive)
						_events.OnNext(new CanSellHotDogEvent());
				});

			_customersEvents
				.OfType<CustomersEvent, NoWaitingCustomerEvent>()
				.Subscribe(_ => _customerWaiting = false);
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public IObserver<CustomersEvent> CustomersObserver => _customersEvents;
		
		public void Sell()
		{
			if (IsSaleActive || !_customerWaiting)
				return;

			StartSale();
			_events.OnNext(new SaleStartedEvent());
			_events.OnNext(new CantSellHotDogEvent());
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

			if (_customerWaiting)
				_events.OnNext(new CanSellHotDogEvent());
		}

		private bool IsSaleActive => _remainingSaleTime.HasValue;

		private bool IsTimeRemainingInSale => _remainingSaleTime > TimeSpan.Zero;

		private void StartSale()
			=> _remainingSaleTime = _sellTime;

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
