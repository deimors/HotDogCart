using System;

namespace Assets.Code.Model.Selling
{
	public class HotDogInABunSoldEvent : HotDogCartEvent, IEquatable<HotDogInABunSoldEvent>
	{
		public bool Equals(HotDogInABunSoldEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as HotDogInABunSoldEvent);

		public override int GetHashCode()
			=> nameof(HotDogInABunSoldEvent).GetHashCode();

		public static bool operator ==(HotDogInABunSoldEvent left, HotDogInABunSoldEvent right)
			=> Equals(left, right);

		public static bool operator !=(HotDogInABunSoldEvent left, HotDogInABunSoldEvent right)
			=> !Equals(left, right);
	}
}