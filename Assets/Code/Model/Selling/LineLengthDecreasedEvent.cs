using System;

namespace Assets.Code.Model.Selling
{
	public class LineLengthDecreasedEvent : CustomersEvent, IEquatable<LineLengthDecreasedEvent>
	{
		public int LineLength { get; }

		public LineLengthDecreasedEvent(int lineLength)
		{
			LineLength = lineLength;
		}

		public bool Equals(LineLengthDecreasedEvent other)
			=> !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || LineLength == other.LineLength);

		public override bool Equals(object obj)
			=> Equals(obj as LineLengthDecreasedEvent);

		public override int GetHashCode()
			=> LineLength;

		public static bool operator ==(LineLengthDecreasedEvent left, LineLengthDecreasedEvent right)
			=> Equals(left, right);

		public static bool operator !=(LineLengthDecreasedEvent left, LineLengthDecreasedEvent right)
			=> !Equals(left, right);
	}
}