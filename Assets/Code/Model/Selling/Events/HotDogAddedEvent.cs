using System;

namespace Assets.Code.Model.Selling.Events
{
	public class HotDogAddedEvent : GrillEvent, IEquatable<HotDogAddedEvent>
	{
		public int Index { get; }

		public HotDogAddedEvent(int index)
		{
			Index = index;
		}

		public bool Equals(HotDogAddedEvent other)
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
			return Equals((HotDogAddedEvent) obj);
		}

		public override int GetHashCode()
		{
			return Index;
		}

		public static bool operator ==(HotDogAddedEvent left, HotDogAddedEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(HotDogAddedEvent left, HotDogAddedEvent right)
		{
			return !Equals(left, right);
		}
	}
}