using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
{
	public abstract class GrillTestFixture : ObserverTestFixture<GrillEvent>
	{
		private Grill _grill;

		private IObserver<GrillEvent> _observer;

		protected abstract TimeSpan CookTime { get; }

		[SetUp]
		public override void Setup()
		{
			_grill = new Grill();

			base.Setup();
		}

		protected override IObservable<GrillEvent> Observable => _grill.Events;

		protected void Act_ProgressTime(TimeSpan duration)
			=> _grill.ProgressTime(duration);

		protected void Act_AddHotDog()
			=> _grill.AddHotDog();

		protected void Act_RemoveCookedHotDog()
			=> _grill.RemoveCookedHotDog();
	}
}