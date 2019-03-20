using System;

namespace Assets.Code.Model.Selling.Events
{
	public class LineLengthIncreasedEvent : CustomersEvent, IEquatable<LineLengthIncreasedEvent>
	{
		public int LineLength { get; }

		public LineLengthIncreasedEvent(int lineLength)
		{
			LineLength = lineLength;
		}

		public bool Equals(LineLengthIncreasedEvent other)
			=> !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || LineLength == other.LineLength);

		public override bool Equals(object obj)
			=> Equals(obj as LineLengthIncreasedEvent);

		public override int GetHashCode()
			=> LineLength;

		public static bool operator ==(LineLengthIncreasedEvent left, LineLengthIncreasedEvent right)
			=> Equals(left, right);

		public static bool operator !=(LineLengthIncreasedEvent left, LineLengthIncreasedEvent right)
			=> !Equals(left, right);
	}
}