using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CanAddHotDogEvent : GrillEvent, IEquatable<CanAddHotDogEvent>
	{
		public bool Equals(CanAddHotDogEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CanAddHotDogEvent);

		public override int GetHashCode()
			=> nameof(CanAddHotDogEvent).GetHashCode();

		public static bool operator ==(CanAddHotDogEvent left, CanAddHotDogEvent right)
			=> Equals(left, right);

		public static bool operator !=(CanAddHotDogEvent left, CanAddHotDogEvent right)
			=> !Equals(left, right);
	}
}