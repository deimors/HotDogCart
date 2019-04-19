using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.TimeTests
{
	public class ProgressTimeTests : TimeTestFixture
	{
		[Test]
		public void TimeProgressed([Random(0, int.MaxValue, 1)]int minutes)
		{
			var duration = TimeSpan.FromMinutes(minutes);

			Act_Progress(duration);

			Assert_EventObserved(new TimeProgressedEvent(duration, DateTime.MinValue + duration));
		}
	}
}