using System;

namespace Assets.Code.Model.Selling
{
	public class CantSellHotDogEvent : HotDogCartEvent, IEquatable<CantSellHotDogEvent>
	{
		public bool Equals(CantSellHotDogEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CantSellHotDogEvent);

		public override int GetHashCode()
			=> nameof(CantSellHotDogEvent).GetHashCode();

		public static bool operator ==(CantSellHotDogEvent left, CantSellHotDogEvent right)
			=> Equals(left, right);

		public static bool operator !=(CantSellHotDogEvent left, CantSellHotDogEvent right)
			=> !Equals(left, right);
	}
}