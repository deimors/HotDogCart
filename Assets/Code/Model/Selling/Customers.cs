using System;
using UniRx;

namespace Assets.Code.Model.Selling
{
	public class Customers
	{
		private readonly TimeSpan _sellTime;
		private readonly ISubject<CustomersEvent> _events = new Subject<CustomersEvent>();

		private TimeSpan? _remainingSaleTime;
		private bool _customerWaiting;

		public IObservable<CustomersEvent> Events => _events;

		public void AddWaitingCustomer()
		{
			if (!_customerWaiting)
			{
				_customerWaiting = true;
				_events.OnNext(new CustomerStartedWaitingEvent());
			}
			else
			{
				_events.OnNext(new PotentialCustomerWalkedAwayEvent());
			}
		}
	}
}