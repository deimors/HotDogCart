using System;

namespace Assets.Code.Model.Selling.Events
{
	public class NoCookedHotDogsAvailableEvent : GrillEvent, IEquatable<NoCookedHotDogsAvailableEvent>
	{
		public bool Equals(NoCookedHotDogsAvailableEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as NoCookedHotDogsAvailableEvent);

		public override int GetHashCode()
			=> nameof(NoCookedHotDogsAvailableEvent).GetHashCode();

		public static bool operator ==(NoCookedHotDogsAvailableEvent left, NoCookedHotDogsAvailableEvent right)
			=> Equals(left, right);

		public static bool operator !=(NoCookedHotDogsAvailableEvent left, NoCookedHotDogsAvailableEvent right)
			=> !Equals(left, right);
	}
}