using System;

namespace Assets.Code.Model.Selling.Events
{
	public class TimeProgressedEvent : HotDogCartEvent, IEquatable<TimeProgressedEvent>
	{
		public TimeSpan Duration { get; }

		public TimeProgressedEvent(TimeSpan duration)
		{
			Duration = duration;
		}

		public bool Equals(TimeProgressedEvent other)
			=> !(other is null) 
			&& (ReferenceEquals(this, other) || Duration.Equals(other.Duration));

		public override bool Equals(object obj)
			=> Equals(obj as TimeProgressedEvent);

		public override int GetHashCode()
			=> Duration.GetHashCode();

		public static bool operator ==(TimeProgressedEvent left, TimeProgressedEvent right)
			=> Equals(left, right);

		public static bool operator !=(TimeProgressedEvent left, TimeProgressedEvent right)
			=> !Equals(left, right);
	}
}