using System;

namespace Assets.Code.Model.Selling
{
	public class CantSellEvent : HotDogCartEvent, IEquatable<CantSellEvent>
	{
		public bool Equals(CantSellEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CantSellEvent);

		public override int GetHashCode()
			=> nameof(CantSellEvent).GetHashCode();

		public static bool operator ==(CantSellEvent left, CantSellEvent right)
			=> Equals(left, right);

		public static bool operator !=(CantSellEvent left, CantSellEvent right)
			=> !Equals(left, right);
	}
}