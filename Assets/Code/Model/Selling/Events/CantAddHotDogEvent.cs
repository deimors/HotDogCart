using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CantAddHotDogEvent : GrillEvent, IEquatable<CantAddHotDogEvent>
	{
		public bool Equals(CantAddHotDogEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CantAddHotDogEvent);

		public override int GetHashCode()
			=> nameof(CantAddHotDogEvent).GetHashCode();

		public static bool operator ==(CantAddHotDogEvent left, CantAddHotDogEvent right)
			=> Equals(left, right);

		public static bool operator !=(CantAddHotDogEvent left, CantAddHotDogEvent right)
			=> !Equals(left, right);
	}
}