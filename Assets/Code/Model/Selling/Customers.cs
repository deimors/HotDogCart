using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Customers
	{
		private readonly TimeSpan _sellTime;
		private readonly ISubject<CustomersEvent> _events = new Subject<CustomersEvent>();
		private readonly ISubject<HotDogCartEvent> _cartEvents = new Subject<HotDogCartEvent>();

		private bool _customerWaiting;

		public IObservable<CustomersEvent> Events => _events;

		public IObserver<HotDogCartEvent> CartObserver => _cartEvents;

		public Customers()
		{
			_cartEvents
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.Subscribe(_ => RemoveWaitingCustomer());
		}
		
		public void AddWaitingCustomer()
		{
			if (!_customerWaiting)
			{
				_customerWaiting = true;
				_events.OnNext(new CustomerStartedWaitingEvent());
			}
			else
			{
				_events.OnNext(new MissedCustomerEvent());
			}
		}

		private void RemoveWaitingCustomer()
		{
			_customerWaiting = false;
			_events.OnNext(new NoWaitingCustomerEvent());
		}
	}
}