using System;
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
		private readonly ISubject<TimeEvent> _timeEvents = new Subject<TimeEvent>();

		private readonly ISubject<bool> _saleActive = new Subject<bool>();
		
		private TimeSpan? _remainingSaleTime;

		private bool _canSell;
		
		public HotDogCart(TimeSpan sellTime)
		{
			_sellTime = sellTime;

			var canSellStream = CustomersAvailable
				.CombineLatest(HotDogsAvailable, _saleActive, (customer, hotDog, saleActive) => customer && hotDog && !saleActive)
				.DistinctUntilChanged();

			canSellStream
				.Subscribe(canSell => _canSell = canSell);

			canSellStream
				.Select(canSell => canSell ? new CanSellEvent() as HotDogCartEvent : new CantSellEvent())
				.Subscribe(_events);

			_timeEvents
				.OfType<TimeEvent, TimeProgressedEvent>()
				.Select(e => e.Duration)
				.Subscribe(ProgressTime);
			
			_saleActive.OnNext(false);
		}

		private IObservable<bool> HotDogsAvailable 
			=> _grillEvents
				.OfType<GrillEvent, CookedHotDogsAvailableEvent>()
				.Select(_ => true)
				.Merge(
					_grillEvents
						.OfType<GrillEvent, NoCookedHotDogsAvailableEvent>()
						.Select(_ => false)
				);

		private IObservable<bool> CustomersAvailable 
			=> _customersEvents
				.OfType<CustomersEvent, LineNotEmptyEvent>()
				.Select(_ => true)
				.Merge(
					_customersEvents
						.OfType<CustomersEvent, LineEmptyEvent>()
						.Select(_ => false)
				);

		public IObservable<HotDogCartEvent> Events => _events;

		public IObserver<CustomersEvent> CustomersObserver => _customersEvents;

		public IObserver<GrillEvent> GrillObserver => _grillEvents;

		public IObserver<TimeEvent> TimeObserver => _timeEvents;

		public void Sell()
			=> Observable.Create<Unit>(observer => { observer.OnNext(Unit.Default); return Disposable.Empty; })
				.Where(_ => _canSell)
				.Do(_ => StartSale())
				.Select(_ => new SaleStartedEvent())
				.Subscribe(_events);

		private void ProgressTime(TimeSpan duration)
		{
			if (!IsSaleActive)
				return;

			ReduceRemainingSaleTime(duration);

			_events.OnNext(new SaleProgressedEvent(Progress));

			if (IsTimeRemainingInSale)
				return;

			CompleteSale();
			
			_events.OnNext(new SaleCompletedEvent());
		}

		private bool IsSaleActive => _remainingSaleTime.HasValue;

		private bool IsTimeRemainingInSale => _remainingSaleTime > TimeSpan.Zero;

		private void StartSale()
		{
			_remainingSaleTime = _sellTime;
			_saleActive.OnNext(IsSaleActive);
		}

		private void CompleteSale()
		{
			_remainingSaleTime = null;
			_saleActive.OnNext(IsSaleActive);
		}

		private void ReduceRemainingSaleTime(TimeSpan duration)
			=> _remainingSaleTime -= duration;

		private float Progress
			=> _remainingSaleTime.HasValue 
				? 1f - (float)(_remainingSaleTime.Value.TotalMilliseconds / _sellTime.TotalMilliseconds) 
				: 0;
	}
}
