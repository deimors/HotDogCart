using Assets.Code.Model.Selling.Events;
using NUnit.Framework;
using System;

namespace Assets.Code.Model.Selling.Tests.TimeTests
{
	public class TimeTestFixture : ObserverTestFixture<TimeEvent>
	{
		private Time _time;

		protected override IObservable<TimeEvent> Observable => _time.Events;

		[SetUp]
		public override void Setup()
		{
			_time = new Time();

			base.Setup();
		}

		protected void Act_Progress(TimeSpan duration)
			=> _time.Progress(duration);
	}
}
