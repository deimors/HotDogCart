using System;

namespace Assets.Code.Model.Selling.Events
{
	public class SaleProgressedEvent : HotDogCartEvent, IEquatable<SaleProgressedEvent>
	{
		public float Progress { get; }

		public SaleProgressedEvent(float progress)
		{
			Progress = progress;
		}

		public bool Equals(SaleProgressedEvent other)
			=> !(other is null) 
			&& (ReferenceEquals(this, other) || Progress.Equals(other.Progress));

		public override bool Equals(object obj)
			=> Equals(obj as SaleProgressedEvent);

		public override int GetHashCode()
			=> Progress.GetHashCode();

		public static bool operator ==(SaleProgressedEvent left, SaleProgressedEvent right)
			=> Equals(left, right);

		public static bool operator !=(SaleProgressedEvent left, SaleProgressedEvent right)
			=> !Equals(left, right);
	}
}