using System;

namespace Assets.Code.Model.Selling.Events
{
	public class SaleStartedEvent : HotDogCartEvent, IEquatable<SaleStartedEvent>
	{
		public bool Equals(SaleStartedEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as SaleStartedEvent);

		public override int GetHashCode()
			=> nameof(SaleStartedEvent).GetHashCode();

		public static bool operator ==(SaleStartedEvent left, SaleStartedEvent right)
			=> Equals(left, right);

		public static bool operator !=(SaleStartedEvent left, SaleStartedEvent right)
			=> !Equals(left, right);
	}
}