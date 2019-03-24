using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CookedHotDogAvailableEvent : GrillEvent, IEquatable<CookedHotDogAvailableEvent>
	{
		public bool Equals(CookedHotDogAvailableEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as CookedHotDogAvailableEvent);

		public override int GetHashCode()
			=> nameof(CookedHotDogAvailableEvent).GetHashCode();

		public static bool operator ==(CookedHotDogAvailableEvent left, CookedHotDogAvailableEvent right)
			=> Equals(left, right);

		public static bool operator !=(CookedHotDogAvailableEvent left, CookedHotDogAvailableEvent right)
			=> !Equals(left, right);
	}
}