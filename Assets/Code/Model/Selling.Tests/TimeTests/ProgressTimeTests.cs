using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.TimeTests
{
	public class ProgressTimeTests : TimeTestFixture
	{
		[Test]
		public void TimeProgressed()
		{
			var duration = TimeSpan.FromMinutes(3);

			Act_Progress(duration);
		}
	}
}