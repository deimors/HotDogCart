using System;

namespace Assets.Code.Model.Selling
{
	public class NoWaitingCustomerEvent : CustomersEvent, IEquatable<NoWaitingCustomerEvent>
	{
		public bool Equals(NoWaitingCustomerEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as NoWaitingCustomerEvent);

		public override int GetHashCode()
			=> nameof(NoWaitingCustomerEvent).GetHashCode();

		public static bool operator ==(NoWaitingCustomerEvent left, NoWaitingCustomerEvent right)
			=> Equals(left, right);

		public static bool operator !=(NoWaitingCustomerEvent left, NoWaitingCustomerEvent right)
			=> !Equals(left, right);
	}
}