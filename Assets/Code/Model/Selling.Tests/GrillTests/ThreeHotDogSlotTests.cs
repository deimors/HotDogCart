using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
{
	public class ThreeHotDogSlotTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		protected override int SlotCount => 3;

		[Test]
		public void AddTwoHotDogs()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventNotObserved(Arg.Any<CantAddHotDogEvent>());
		}

		[Test]
		public void AddThreeHotDogs()
		{
			Act_AddHotDog();
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventObserved(new CantAddHotDogEvent());
		}

		[Test]
		public void AddThreeThenRemoveOne()
		{
			Act_AddHotDog();
			Act_AddHotDog();
			Act_AddHotDog();

			Act_RemoveCookedHotDog();

			Assert_EventNotObserved(Arg.Any<CanAddHotDogEvent>());
			Assert_EventNotObserved(Arg.Any<CookedHotDogRemovedEvent>());
		}
	}
}