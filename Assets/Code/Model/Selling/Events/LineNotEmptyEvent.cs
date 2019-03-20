using System;

namespace Assets.Code.Model.Selling.Events
{
	public class LineNotEmptyEvent : CustomersEvent, IEquatable<LineNotEmptyEvent>
	{
		public bool Equals(LineNotEmptyEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as LineNotEmptyEvent);

		public override int GetHashCode()
			=> nameof(LineNotEmptyEvent).GetHashCode();

		public static bool operator ==(LineNotEmptyEvent left, LineNotEmptyEvent right)
			=> Equals(left, right);

		public static bool operator !=(LineNotEmptyEvent left, LineNotEmptyEvent right)
			=> !Equals(left, right);
	}
}