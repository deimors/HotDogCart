using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Customers
	{
		private readonly int _maxLineLength;
		private readonly ISubject<CustomersEvent> _events = new Subject<CustomersEvent>();
		private readonly ISubject<HotDogCartEvent> _cartEvents = new Subject<HotDogCartEvent>();

		private int _lineLength;

		public IObservable<CustomersEvent> Events => _events;

		public IObserver<HotDogCartEvent> CartObserver => _cartEvents;

		public Customers(int maxLineLength)
		{
			_maxLineLength = maxLineLength;

			_cartEvents
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.Subscribe(_ => RemoveWaitingCustomer());
		}
		
		public void AddWaitingCustomer()
		{
			if (_lineLength < _maxLineLength)
			{
				_lineLength++;
				_events.OnNext(new LineNotEmptyEvent());
			}
			else
			{
				_events.OnNext(new MissedCustomerEvent());
			}
		}

		private void RemoveWaitingCustomer()
		{
			_lineLength--;

			if (_lineLength == 0)
				_events.OnNext(new LineEmptyEvent());
		}
	}
}