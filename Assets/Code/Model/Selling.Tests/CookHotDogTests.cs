using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CookHotDogTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void AddHotDog()
		{
			Act_AddHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0)
			);

			Assert_EventNotObserved(new CookingProgressedEvent(1.0f));
			Assert_EventNotObserved(new HotDogCookedEvent(0));
		}

		[Test]
		public void AddHotDogAndHalfCook()
		{
			Act_AddHotDog();

			Act_ProgressTime(HalfCookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(.5f)
			);

			Assert_EventNotObserved(new HotDogCookedEvent(0));
		}

		[Test]
		public void AddHotDogAndCookCompletely()
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
	}
}