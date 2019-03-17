using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Customers
	{
		private readonly int _lineLength;
		private readonly ISubject<CustomersEvent> _events = new Subject<CustomersEvent>();
		private readonly ISubject<HotDogCartEvent> _cartEvents = new Subject<HotDogCartEvent>();

		private int _customersWaiting;

		public IObservable<CustomersEvent> Events => _events;

		public IObserver<HotDogCartEvent> CartObserver => _cartEvents;

		public Customers(int lineLength)
		{
			_lineLength = lineLength;

			_cartEvents
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.Subscribe(_ => RemoveWaitingCustomer());
		}
		
		public void AddWaitingCustomer()
		{
			if (_customersWaiting < _lineLength)
			{
				_customersWaiting++;
				_events.OnNext(new CustomerStartedWaitingEvent());
			}
			else
			{
				_events.OnNext(new MissedCustomerEvent());
			}
		}

		private void RemoveWaitingCustomer()
		{
			_customersWaiting--;

			if (_customersWaiting == 0)
				_events.OnNext(new NoWaitingCustomerEvent());
		}
	}
}