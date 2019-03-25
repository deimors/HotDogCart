﻿using System;
using Assets.Code.Model.Selling.Events;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class HotDogCart
	{
		private readonly TimeSpan _sellTime;

		private readonly ISubject<HotDogCartEvent> _events = new Subject<HotDogCartEvent>();
		private readonly ISubject<CustomersEvent> _customersEvents = new Subject<CustomersEvent>();
		private readonly ISubject<GrillEvent> _grillEvents = new Subject<GrillEvent>();
		
		private TimeSpan? _remainingSaleTime;
		private bool _customerWaiting;
		private bool _cookedHotDogAvailable;

		public HotDogCart(TimeSpan sellTime)
		{
			_sellTime = sellTime;

			_customersEvents
				.OfType<CustomersEvent, LineNotEmptyEvent>()
				.Subscribe(_ =>
				{
					_customerWaiting = true;

					if (!IsSaleActive && _cookedHotDogAvailable)
						_events.OnNext(new CanSellEvent());
				});

			_customersEvents
				.OfType<CustomersEvent, LineEmptyEvent>()
				.Subscribe(_ => _customerWaiting = false);

			_grillEvents
				.OfType<GrillEvent, CookedHotDogsAvailableEvent>()
				.Subscribe(_ =>
				{
					_cookedHotDogAvailable = true;

					if (!IsSaleActive && _customerWaiting)
						_events.OnNext(new CanSellEvent());
				});
		}

		public IObservable<HotDogCartEvent> Events => _events;

		public IObserver<CustomersEvent> CustomersObserver => _customersEvents;

		public IObserver<GrillEvent> GrillObserver => _grillEvents;

		public void Sell()
		{
			if (IsSaleActive || !_customerWaiting || !_cookedHotDogAvailable)
				return;

			StartSale();
			_events.OnNext(new SaleStartedEvent());
			_events.OnNext(new CantSellEvent());
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
			
			_events.OnNext(new SaleCompletedEvent());

			if (_customerWaiting)
				_events.OnNext(new CanSellEvent());
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
