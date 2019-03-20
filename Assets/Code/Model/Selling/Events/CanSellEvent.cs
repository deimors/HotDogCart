using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CanSellEvent : HotDogCartEvent, IEquatable<CanSellEvent>
	{
		public bool Equals(CanSellEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CanSellEvent);

		public override int GetHashCode()
			=> nameof(CanSellEvent).GetHashCode();

		public static bool operator ==(CanSellEvent left, CanSellEvent right)
			=> Equals(left, right);

		public static bool operator !=(CanSellEvent left, CanSellEvent right)
			=> !Equals(left, right);
	}
}