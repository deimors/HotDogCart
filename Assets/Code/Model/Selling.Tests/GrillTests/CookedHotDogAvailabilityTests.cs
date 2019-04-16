using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
{
	public class CookedHotDogAvailabilityTests : GrillTestFixture
	{
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void CookOne()
		{
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogsAvailableEvent()
			);
		}

		[Test]
		public void CookTwo()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogsAvailableEvent(),
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
				new CookedHotDogsAvailableEvent(),
				new CookedHotDogRemovedEvent(0),
				new NoCookedHotDogsAvailableEvent()
			);
		}

		[Test]
		public void CookTwoAndRemoveOne()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogsAvailableEvent()
			);

			Assert_EventNotObserved(Arg.Any<NoCookedHotDogsAvailableEvent>());
		}

		[Test]
		public void CookTwoAndRemoveTwo()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();
			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogCookedEvent(0),
				new CookedHotDogsAvailableEvent(),
				new HotDogCookedEvent(1),
				new CookedHotDogRemovedEvent(0),
				new CookedHotDogRemovedEvent(1),
				new NoCookedHotDogsAvailableEvent()
			);
		}
	}
}