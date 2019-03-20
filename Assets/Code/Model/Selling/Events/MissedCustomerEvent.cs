using System;

namespace Assets.Code.Model.Selling.Events
{
	public class MissedCustomerEvent : CustomersEvent, IEquatable<MissedCustomerEvent>
	{
		public bool Equals(MissedCustomerEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as MissedCustomerEvent);

		public override int GetHashCode()
			=> nameof(MissedCustomerEvent).GetHashCode();

		public static bool operator ==(MissedCustomerEvent left, MissedCustomerEvent right)
			=> Equals(left, right);

		public static bool operator !=(MissedCustomerEvent left, MissedCustomerEvent right)
			=> !Equals(left, right);
	}
}