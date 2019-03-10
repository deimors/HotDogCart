using System;

namespace Assets.Code.Model.Selling
{
	public class CustomerStartedWaitingEvent : CustomersEvent, IEquatable<CustomerStartedWaitingEvent>
	{
		public bool Equals(CustomerStartedWaitingEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CustomerStartedWaitingEvent);

		public override int GetHashCode()
			=> nameof(CustomerStartedWaitingEvent).GetHashCode();

		public static bool operator ==(CustomerStartedWaitingEvent left, CustomerStartedWaitingEvent right)
			=> Equals(left, right);

		public static bool operator !=(CustomerStartedWaitingEvent left, CustomerStartedWaitingEvent right)
			=> !Equals(left, right);
	}
}