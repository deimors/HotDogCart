using System;

namespace Assets.Code.Model.Selling.Events
{
	public class HotDogCookedEvent : GrillEvent, IEquatable<HotDogCookedEvent>
	{
		public int Index { get; }

		public HotDogCookedEvent(int index)
		{
			Index = index;
		}

		public bool Equals(HotDogCookedEvent other)
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
			return Equals((HotDogCookedEvent) obj);
		}

		public override int GetHashCode()
		{
			return Index;
		}

		public static bool operator ==(HotDogCookedEvent left, HotDogCookedEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(HotDogCookedEvent left, HotDogCookedEvent right)
		{
			return !Equals(left, right);
		}
	}
}