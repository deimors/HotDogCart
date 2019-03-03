using System;

namespace Assets.Code.Model.Selling
{
	public class CustomerWalkedAwayEvent : HotDogCartEvent, IEquatable<CustomerWalkedAwayEvent>
	{
		public bool Equals(CustomerWalkedAwayEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CustomerWalkedAwayEvent);

		public override int GetHashCode()
			=> nameof(CustomerWalkedAwayEvent).GetHashCode();

		public static bool operator ==(CustomerWalkedAwayEvent left, CustomerWalkedAwayEvent right)
			=> Equals(left, right);

		public static bool operator !=(CustomerWalkedAwayEvent left, CustomerWalkedAwayEvent right)
			=> !Equals(left, right);
	}
}