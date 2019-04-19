using System;
using Assets.Code.Model.Selling.Events;
using NUnit.Framework;

namespace Assets.Code.Model.Selling.Tests.GrillTests
{
	public abstract class GrillTestFixture : ObserverTestFixture<GrillEvent>
	{
		private Grill _grill;

		private IObserver<GrillEvent> _observer;

		protected abstract TimeSpan CookTime { get; }

		protected virtual int SlotCount { get; } = 2;

		[SetUp]
		public override void Setup()
		{
			_grill = new Grill(SlotCount);

			base.Setup();
		}

		protected override IObservable<GrillEvent> Observable => _grill.Events;

		protected void Arrange_TimeProgressed(TimeSpan duration)
			=> Arrange_TimeEvent(new TimeProgressedEvent(duration, default(DateTime)));

		protected void Arrange_TimeEvent(TimeEvent timeEvent)
			=> _grill.TimeObserver.OnNext(timeEvent);

		protected void Act_AddHotDog()
			=> _grill.AddHotDog();

		protected void Act_RemoveCookedHotDog()
			=> _grill.RemoveCookedHotDog();
	}
}