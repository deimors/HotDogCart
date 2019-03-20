using System;

namespace Assets.Code.Model.Selling.Events
{
	public class LineEmptyEvent : CustomersEvent, IEquatable<LineEmptyEvent>
	{
		public bool Equals(LineEmptyEvent other)
			=> !(other is null);

		public override bool Equals(object obj)
			=> Equals(obj as LineEmptyEvent);

		public override int GetHashCode()
			=> nameof(LineEmptyEvent).GetHashCode();

		public static bool operator ==(LineEmptyEvent left, LineEmptyEvent right)
			=> Equals(left, right);

		public static bool operator !=(LineEmptyEvent left, LineEmptyEvent right)
			=> !Equals(left, right);
	}
}