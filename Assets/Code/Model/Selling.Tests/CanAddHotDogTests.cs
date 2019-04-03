using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class CanAddHotDogTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime { get; }

		[Test]
		public void AddTwoHotDogs()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventObserved(new CantAddHotDogEvent());
		}

		[Test]
		public void AddTwoAndRemoveOne()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new CantAddHotDogEvent(),
				new CanAddHotDogEvent()
			);
		}

		[Test]
		public void AddTwoAndRemoveTwo()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Act_RemoveCookedHotDog();
			Act_RemoveCookedHotDog();

			Assert_EventsObserved(
				new CantAddHotDogEvent(),
				new CanAddHotDogEvent()
			);
		}
	}
}