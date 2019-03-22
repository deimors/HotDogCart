using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class RemoveCookedHotDogTests : GrillTestFixture
	{
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void CookAndRemoveHotDog()
		{
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookedHotDogRemovedEvent(0)
			);
		}

		[Test]
		public void CookTwoHotDogsAndRemoveBoth()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();
			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new HotDogAddedEvent(1),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookingProgressedEvent(1, 1),
				new HotDogCookedEvent(1),
				new CookedHotDogRemovedEvent(0),
				new CookedHotDogRemovedEvent(1)
			);
		}
	}
}