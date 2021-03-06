﻿using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
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

			Assert_EventNotObserved(new CookingProgressedEvent(0, 1.0f));
			Assert_EventNotObserved(new HotDogCookedEvent(0));
		}

		[Test]
		public void AddHotDogAndHalfCook()
		{
			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, .5f)
			);

			Assert_EventNotObserved(new HotDogCookedEvent(0));
		}

		[Test]
		public void AddHotDogAndHalfCookTwice()
		{
			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);
			Arrange_TimeProgressed(HalfCookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, .5f),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0)
			);
		}

		[Test]
		public void AddHotDogAndCookCompletely()
		{
			Act_AddHotDog();

			Arrange_TimeProgressed(CookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(0, 1.0f),
				new HotDogCookedEvent(0)
			);
		}

		[Test]
		public void AddHotDogTwice()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new HotDogAddedEvent(1)
			);
		}

		[Test]
		public void AddTwoHotDogsAndCookBothCompletely()
		{
			Act_AddHotDog();
			Act_AddHotDog();

			Arrange_TimeProgressed(CookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new HotDogAddedEvent(1),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookingProgressedEvent(1, 1),
				new HotDogCookedEvent(1)
			);
		}

		[Test]
		public void AddTwoHotDogsStaggeredAndCookFirstCompletely()
		{
			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);

			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(index: 0, 0.5f),
				new HotDogAddedEvent(1),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookingProgressedEvent(1, .5f)
			);
		}

		[Test]
		public void AddTwoHotDogsStaggeredAndCookBothCompletely()
		{
			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);

			Act_AddHotDog();

			Arrange_TimeProgressed(HalfCookTime);
			Arrange_TimeProgressed(HalfCookTime);

			Assert_EventsObserved(
				new HotDogAddedEvent(0),
				new CookingProgressedEvent(index: 0, 0.5f),
				new HotDogAddedEvent(1),
				new CookingProgressedEvent(0, 1),
				new HotDogCookedEvent(0),
				new CookingProgressedEvent(1, .5f),
				new CookingProgressedEvent(1, 1),
				new HotDogCookedEvent(1)
			);
		}
	}
}