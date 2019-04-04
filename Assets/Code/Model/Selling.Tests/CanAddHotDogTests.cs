using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CanAddHotDogTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void AddTwoHotDogs()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventObserved(new CantAddHotDogEvent());
		}

		[Test]
		public void AddTwoThenRemoveOne()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_RemoveCookedHotDog();

			Assert_EventNotObserved(Arg.Any<CanAddHotDogEvent>());
			Assert_EventNotObserved(Arg.Any<CookedHotDogRemovedEvent>());
		}

		[Test]
		public void CookTwoThenRemoveOne()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new CantAddHotDogEvent(),
				new CanAddHotDogEvent()
			);
		}

		[Test]
		public void CookTwoThenRemoveTwo()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();
			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new CantAddHotDogEvent(),
				new CanAddHotDogEvent()
			);
		}

		[Test]
		public void AddToFirstSlotToFill()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_ProgressTime(CookTime);

			Act_RemoveCookedHotDog();
			
			Act_AddHotDog();

			Assert_EventsObserved(
				new CantAddHotDogEvent(),
				new CanAddHotDogEvent(),
				new CantAddHotDogEvent()
			);
		}
	}
}