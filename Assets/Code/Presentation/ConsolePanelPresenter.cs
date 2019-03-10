using System;
using Assets.Code.Model.Selling;
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
			LogOnCartEvent<HotDogSoldEvent>("Hot Dog Sold");

			LogOnCustomersEvent<CustomerStartedWaitingEvent>("Customer Waiting");

			LogOnCustomersEvent<PotentialCustomerWalkedAwayEvent>("Potential Customer Walked Away");
		}

		private void LogOnCartEvent<TEvent>(string message) where TEvent : HotDogCartEvent
			=> LogOnEvent<HotDogCartEvent, TEvent>(Cart.Events, message);

		private void LogOnCustomersEvent<TEvent>(string message) where TEvent : CustomersEvent
			=> LogOnEvent<CustomersEvent, TEvent>(Customers.Events, message);

		private void LogOnEvent<TBaseEvent, TEvent>(IObservable<TBaseEvent> events, string message) where TEvent : TBaseEvent
			=> events
				.OfType<TBaseEvent, TEvent>()
				.Select(_ => Math.Abs(Scroll.verticalNormalizedPosition) < Mathf.Epsilon)
				.Do(_ => LogText.text += LogText.text == string.Empty ? message : "\n" + message)
				.DelayFrame(1)
				.Where(atScrollEnd => atScrollEnd)
				.Subscribe(_ => Scroll.verticalNormalizedPosition = 0);
	}
}