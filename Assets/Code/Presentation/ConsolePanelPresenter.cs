using System;
using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Assets.Code.Presentation
{
	public class ConsolePanelPresenter : MonoBehaviour
	{
		public Text LogText;
		public ScrollRect Scroll;

		[Inject]
		public HotDogCart Cart { private get; set; }

		[Inject]
		public Customers Customers { private get; set; }

		[Inject]
		public void Initialize()
		{
			LogOnCartEvent<SaleCompletedEvent>(_ => "Hot Dog Sold");

			LogOnCustomersEvent<LineNotEmptyEvent>(_ => "Line Not Empty");
			LogOnCustomersEvent<LineEmptyEvent>(_ => "Line Empty");

			LogOnCustomersEvent<LineLengthIncreasedEvent>(e => $"Line Increased to {e.LineLength}");
			LogOnCustomersEvent<LineLengthDecreasedEvent>(e => $"Line Decreased to {e.LineLength}");

			LogOnCustomersEvent<MissedCustomerEvent>(_ => "Missed Customer");
		}

		private void LogOnCartEvent<TEvent>(Func<TEvent, string> message) where TEvent : HotDogCartEvent
			=> LogOnEvent<HotDogCartEvent, TEvent>(Cart.Events, message);

		private void LogOnCustomersEvent<TEvent>(Func<TEvent, string> message) where TEvent : CustomersEvent
			=> LogOnEvent<CustomersEvent, TEvent>(Customers.Events, message);

		private void LogOnEvent<TBaseEvent, TEvent>(IObservable<TBaseEvent> events, Func<TEvent, string> message) where TEvent : TBaseEvent
			=> events
				.OfType<TBaseEvent, TEvent>()
				.Select(e => new { e, atScrollEnd = Math.Abs(Scroll.verticalNormalizedPosition) < Mathf.Epsilon}) 
				.Do(a => LogText.text += LogText.text == string.Empty ? message(a.e) : "\n" + message(a.e))
				.DelayFrame(1)
				.Where(a => a.atScrollEnd)
				.Subscribe(_ => Scroll.verticalNormalizedPosition = 0);
	}
}