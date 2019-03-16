using System;

namespace Assets.Code.Model.Selling
{
	public class PotentialCustomerWalkedAwayEvent : CustomersEvent, IEquatable<PotentialCustomerWalkedAwayEvent>
	{
		public bool Equals(PotentialCustomerWalkedAwayEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as PotentialCustomerWalkedAwayEvent);

		public override int GetHashCode()
			=> nameof(PotentialCustomerWalkedAwayEvent).GetHashCode();

		public static bool operator ==(PotentialCustomerWalkedAwayEvent left, PotentialCustomerWalkedAwayEvent right)
			=> Equals(left, right);

		public static bool operator !=(PotentialCustomerWalkedAwayEvent left, PotentialCustomerWalkedAwayEvent right)
			=> !Equals(left, right);
	}
}