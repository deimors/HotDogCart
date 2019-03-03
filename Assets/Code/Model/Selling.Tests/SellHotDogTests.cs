﻿using System;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests
{
	public class SellHotDogTests : HotDogCartTestFixture
	{
		[Test]
		public void SellAndDontProgressTime()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			Assert_EventObserved(new SaleStartedEvent());
			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinute()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new TimeProgressedEvent(duration),
				new HotDogSoldEvent()
			);
		}
		
		[Test]
		public void DontSellAndProgressTimeOneMinute()
		{
			Act_AddWaitingCustomer();

			var duration = TimeSpan.FromMinutes(1);
			Act_ProgressTime(duration);

			Assert_EventObserved(new TimeProgressedEvent(duration));
			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeOneMinuteTwice()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Act_ProgressTime(TimeSpan.FromMinutes(1));

			Assert_EventObserved(new HotDogSoldEvent(), 1);
		}

		[Test]
		public void SellAndProgressTimeHalfAMinute()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventNotObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellWhenAlreadySelling()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Act_AddWaitingCustomer();

			Act_Sell();

			Act_ProgressTime(TimeSpan.FromSeconds(30));

			Assert_EventObserved(new SaleStartedEvent(), 1);
			Assert_EventObserved(new HotDogSoldEvent());
		}

		[Test]
		public void SellAndProgressTimeHalfAMinuteTwice()
		{
			Act_AddWaitingCustomer();

			Act_Sell();

			var halfMinute = TimeSpan.FromSeconds(30);

			Act_ProgressTime(halfMinute);
			Act_ProgressTime(halfMinute);

			Assert_EventsObserved(
				new SaleStartedEvent(),
				new SaleProgressedEvent(0.5f),
				new SaleProgressedEvent(1.0f),
				new HotDogSoldEvent()
			);
		}

		[Test]
		public void SellWithoutWaitingCustomer()
		{
			Act_Sell();

			Assert_EventNotObserved(new SaleStartedEvent());
		}
	}
}
