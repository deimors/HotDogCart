using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CookHotDogTests : GrillTestFixture
	{
		[Test]
		public void AddHotDogAndCookIt()
		{
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(1.0f),
				new HotDogCookedEvent(0)
			);
		}
	}

	public class GrillTestFixture : ObserverTestFixture<GrillEvent>
	{
		private Grill _grill;

		private IObserver<GrillEvent> _observer;

		protected virtual TimeSpan CookTime { get; } = TimeSpan.FromMinutes(5);

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
	}
}