using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CookedHotDogsAvailableEvent : GrillEvent, IEquatable<CookedHotDogsAvailableEvent>
	{
		public bool Equals(CookedHotDogsAvailableEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CookedHotDogsAvailableEvent);

		public override int GetHashCode()
			=> nameof(CookedHotDogsAvailableEvent).GetHashCode();

		public static bool operator ==(CookedHotDogsAvailableEvent left, CookedHotDogsAvailableEvent right)
			=> Equals(left, right);

		public static bool operator !=(CookedHotDogsAvailableEvent left, CookedHotDogsAvailableEvent right)
			=> !Equals(left, right);
	}
}