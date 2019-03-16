using System;
using Assets.Code.Model.Selling;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.Presentation
{
	public class SellButtonPresenter : MonoBehaviour, IPointerClickHandler
	{
		public Button SellButton;

		public Slider ProgressSlider;
		private bool _waiting;

		[Inject]
		public HotDogCart Cart { private get; set; }
		
		[Inject]
		public void Initialize()
		{
			HideProgressSlider();
			DisableSellButton();

			OnCartEvent<CanSellEvent>(_ => EnableSellButton());

			OnCartEvent<CantSellEvent>(_ => DisableSellButton());

			OnCartEvent<SaleStartedEvent>(_ => ShowProgressSlider());

			OnCartEvent<SaleCompletedEvent>(_ => HideProgressSlider());

			OnCartEvent<SaleProgressedEvent>(e => ProgressSlider.value = e.Progress);
		}

		private void OnCartEvent<TEvent>(Action<TEvent> action) where TEvent : HotDogCartEvent
			=> Cart.Events
				.OfType<HotDogCartEvent, TEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(action);

		public void OnPointerClick(PointerEventData eventData)
		{
			if (SellButton.interactable)
				Cart.Sell();
		}

		private void EnableSellButton()
			=> SellButton.interactable = true;

		private void DisableSellButton()
			=> SellButton.interactable = false;

		private void ShowProgressSlider()
			=> ProgressSlider.gameObject.SetActive(true);

		private void HideProgressSlider()
			=> ProgressSlider.gameObject.SetActive(false);
	}
}
