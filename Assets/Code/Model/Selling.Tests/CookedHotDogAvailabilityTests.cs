using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CookedHotDogAvailabilityTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void CookOneCompletely()
		{
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogAvailableEvent()
			);
		}

		[Test]
		public void CookTwoCompletely()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogAvailableEvent(),
				new HotDogCookedEvent(1)
			);
		}

		[Test]
		public void CookAndRemoveOne()
		{
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogAvailableEvent(),
				new CookedHotDogRemovedEvent(0),
				new NoCookedHotDogsAvailableEvent()
			);
		}
	}
}