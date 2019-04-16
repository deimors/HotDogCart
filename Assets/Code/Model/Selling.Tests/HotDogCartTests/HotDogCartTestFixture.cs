using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.HotDogCartTests
{
	public class HotDogCartTestFixture : ObserverTestFixture<HotDogCartEvent>
	{
		private HotDogCart _pos;

		protected TimeSpan SellTime { get; set; } = TimeSpan.FromMinutes(1);
		
		[SetUp]
		public override void Setup()
		{
			_pos = new HotDogCart(SellTime);
			
			base.Setup();
		}

		protected override IObservable<HotDogCartEvent> Observable => _pos.Events;

		protected void Arrange_CustomersEvent(CustomersEvent customersEvent)
			=> _pos.CustomersObserver.OnNext(customersEvent);

		protected void Arrange_TimeProgressed(TimeSpan duration)
			=> Arrange_TimeEvent(new TimeProgressedEvent(duration));

		private void Arrange_TimeEvent(TimeEvent timeEvent)
			=> _pos.TimeObserver.OnNext(timeEvent);

		protected void Arrange_GrillEvent(GrillEvent grillEvent)
			=> _pos.GrillObserver.OnNext(grillEvent);

		protected void Act_Sell()
			=> _pos.Sell();
		
		protected void Arrange_SellConditionsMet()
		{
			Arrange_GrillEvent(new CookedHotDogsAvailableEvent());
			Arrange_CustomersEvent(new LineNotEmptyEvent());
		}
	}
}