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
	}
}