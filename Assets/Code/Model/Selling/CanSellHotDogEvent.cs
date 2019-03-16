using System;

namespace Assets.Code.Model.Selling
{
	public class CanSellHotDogEvent : HotDogCartEvent, IEquatable<CanSellHotDogEvent>
	{
		public bool Equals(CanSellHotDogEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CanSellHotDogEvent);

		public override int GetHashCode()
			=> nameof(CanSellHotDogEvent).GetHashCode();

		public static bool operator ==(CanSellHotDogEvent left, CanSellHotDogEvent right)
			=> Equals(left, right);

		public static bool operator !=(CanSellHotDogEvent left, CanSellHotDogEvent right)
			=> !Equals(left, right);
	}
}