using System;

namespace Assets.Code.Model.Selling.Events
{
	public class CookingProgressedEvent : GrillEvent, IEquatable<CookingProgressedEvent>
	{
		public int Index { get; }
		public float Progress { get; }

		public CookingProgressedEvent(int index, float progress)
		{
			Index = index;
			Progress = progress;
		}

		public bool Equals(CookingProgressedEvent other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Index == other.Index && Progress.Equals(other.Progress);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CookingProgressedEvent) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Index * 397) ^ Progress.GetHashCode();
			}
		}

		public static bool operator ==(CookingProgressedEvent left, CookingProgressedEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(CookingProgressedEvent left, CookingProgressedEvent right)
		{
			return !Equals(left, right);
		}
	}
}