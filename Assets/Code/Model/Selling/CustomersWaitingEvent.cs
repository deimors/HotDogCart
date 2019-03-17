using System;

namespace Assets.Code.Model.Selling
{
	public class CustomersWaitingEvent : CustomersEvent, IEquatable<CustomersWaitingEvent>
	{
		public bool Equals(CustomersWaitingEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CustomersWaitingEvent);

		public override int GetHashCode()
			=> nameof(CustomersWaitingEvent).GetHashCode();

		public static bool operator ==(CustomersWaitingEvent left, CustomersWaitingEvent right)
			=> Equals(left, right);

		public static bool operator !=(CustomersWaitingEvent left, CustomersWaitingEvent right)
			=> !Equals(left, right);
	}
}