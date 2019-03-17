using System;

namespace Assets.Code.Model.Selling
{
	public class NoWaitingCustomersEvent : CustomersEvent, IEquatable<NoWaitingCustomersEvent>
	{
		public bool Equals(NoWaitingCustomersEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as NoWaitingCustomersEvent);

		public override int GetHashCode()
			=> nameof(NoWaitingCustomersEvent).GetHashCode();

		public static bool operator ==(NoWaitingCustomersEvent left, NoWaitingCustomersEvent right)
			=> Equals(left, right);

		public static bool operator !=(NoWaitingCustomersEvent left, NoWaitingCustomersEvent right)
			=> !Equals(left, right);
	}
}