using System;

namespace Assets.Code.Model.Selling.Events
{
	public class TimeProgressedEvent : TimeEvent, IEquatable<TimeProgressedEvent>
	{
		public TimeSpan Duration { get; }
		public DateTime CurrentTime { get; }

		public TimeProgressedEvent(TimeSpan duration, DateTime currentTime)
		{
			Duration = duration;
			CurrentTime = currentTime;
		}

		public bool Equals(TimeProgressedEvent other)
			=> !ReferenceEquals(null, other) && (ReferenceEquals(this, other)
			|| Duration.Equals(other.Duration) && CurrentTime.Equals(other.CurrentTime));

		public override bool Equals(object obj)
			=> Equals(obj as TimeProgressedEvent);

		public override int GetHashCode()
		{
			unchecked
			{
				return (Duration.GetHashCode() * 397) ^ CurrentTime.GetHashCode();
			}
		}

		public static bool operator ==(TimeProgressedEvent left, TimeProgressedEvent right)
			=> Equals(left, right);

		public static bool operator !=(TimeProgressedEvent left, TimeProgressedEvent right)
			=> !Equals(left, right);
	}
}