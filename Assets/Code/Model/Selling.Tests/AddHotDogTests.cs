using System;
using Assets.Code.Model.Selling.Events;
using NSubstitute;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class AddHotDogTests : GrillTestFixture
	{
		private static readonly TimeSpan HalfCookTime = TimeSpan.FromMinutes(2.5);
		protected override TimeSpan CookTime => TimeSpan.FromMinutes(5);

		[Test]
		public void AddThreeHotDogs()
		{
			Act_AddHotDog();
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventObserved(Arg.Any<HotDogAddedEvent>(), 2);
		}
	}
}