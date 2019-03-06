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
		public void Initialize()
		{
			LogOnEvent<HotDogSoldEvent>("Hot Dog Sold");

			LogOnEvent<CustomerStartedWaitingEvent>("Customer Waiting");

			LogOnEvent<PotentialCustomerWalkedAwayEvent>("Potential Customer Walked Away");
		}

		private void LogOnEvent<TEvent>(string message) where TEvent : HotDogCartEvent
			=> Cart.Events
				.OfType<HotDogCartEvent, TEvent>()
				.Select(_ => Math.Abs(Scroll.verticalNormalizedPosition) < Mathf.Epsilon)
				.Do(_ => LogText.text += LogText.text == string.Empty ? message : "\n" + message)
				.DelayFrame(1)
				.Where(atScrollEnd => atScrollEnd)
				.Subscribe(_ => Scroll.verticalNormalizedPosition = 0);
	}
}