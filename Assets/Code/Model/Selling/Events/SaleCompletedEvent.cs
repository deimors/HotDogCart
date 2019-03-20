using System;

namespace Assets.Code.Model.Selling.Events
{
	public class SaleCompletedEvent : HotDogCartEvent, IEquatable<SaleCompletedEvent>
	{
		public bool Equals(SaleCompletedEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as SaleCompletedEvent);

		public override int GetHashCode()
			=> nameof(SaleCompletedEvent).GetHashCode();

		public static bool operator ==(SaleCompletedEvent left, SaleCompletedEvent right)
			=> Equals(left, right);

		public static bool operator !=(SaleCompletedEvent left, SaleCompletedEvent right)
			=> !Equals(left, right);
	}
}