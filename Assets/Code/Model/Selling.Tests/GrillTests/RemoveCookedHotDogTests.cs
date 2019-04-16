using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
{
	public class RemoveCookedHotDogTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void RemoveHotDogWhenNoneAvailable()
		{
			Act_RemoveCookedHotDog();

			Assert_EventNotObserved(new CookedHotDogRemovedEvent(0));
		}

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

		[Test]
		public void CookAndRemoveHotDogTwice()
		{
			Act_AddHotDog();
			
			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookedHotDogRemovedEvent(0),
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookedHotDogRemovedEvent(0)
			);
		}

		[Test]
		public void HalfCookAndRemoveHotDog()
		{
			Act_AddHotDog();

			Act_ProgressTime(HalfCookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, .5f)
			);

			Assert_EventNotObserved(Arg.Any<CookedHotDogRemovedEvent>());
		}

		[Test]
		public void RemoveCookedHotDogIfAvailable()
		{
			Act_AddHotDog();

			Act_ProgressTime(HalfCookTime);

			Act_AddHotDog();

			Act_ProgressTime(HalfCookTime);

			Act_RemoveCookedHotDog();

			Act_AddHotDog();

			Act_ProgressTime(HalfCookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, .5f),
				new HotDogAddedEvent(1),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookingProgressedEvent(1, .5f),
				new CookedHotDogRemovedEvent(0),
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, .5f),
				new CookingProgressedEvent(1, 1),
				new HotDogCookedEvent(1),
				new CookedHotDogRemovedEvent(1)
			);
		}
	}
}