using System;

namespace Assets.Code.Model.Selling
{
	public class HotDogSoldEvent : HotDogCartEvent, IEquatable<HotDogSoldEvent>
	{
		public bool Equals(HotDogSoldEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as HotDogSoldEvent);

		public override int GetHashCode()
			=> nameof(HotDogSoldEvent).GetHashCode();

		public static bool operator ==(HotDogSoldEvent left, HotDogSoldEvent right)
			=> Equals(left, right);

		public static bool operator !=(HotDogSoldEvent left, HotDogSoldEvent right)
			=> !Equals(left, right);
	}
}