using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CookedHotDogRemovedEvent : GrillEvent, IEquatable<CookedHotDogRemovedEvent>
	{
		public int Index { get; }

		public CookedHotDogRemovedEvent(int index)
		{
			Index = index;
		}

		public bool Equals(CookedHotDogRemovedEvent other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Index == other.Index;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CookedHotDogRemovedEvent) obj);
		}

		public override int GetHashCode()
		{
			return Index;
		}

		public static bool operator ==(CookedHotDogRemovedEvent left, CookedHotDogRemovedEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(CookedHotDogRemovedEvent left, CookedHotDogRemovedEvent right)
		{
			return !Equals(left, right);
		}
	}
}