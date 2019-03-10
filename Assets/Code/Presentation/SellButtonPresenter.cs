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
		public IObservable<CustomersEvent> CustomersEvents { private get; set; }

		[Inject]
		public void Initialize()
		{
			HideProgressSlider();
			DisableSellButton();

			CustomersEvents
				.OfType<CustomersEvent, CustomerStartedWaitingEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => OnCustomerWaiting());

			Cart.Events
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => OnSaleStarted());

			Cart.Events
				.OfType<HotDogCartEvent, HotDogSoldEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => OnSold());

			Cart.Events
				.OfType<HotDogCartEvent, SaleProgressedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(e => ProgressSlider.value = e.Progress);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (SellButton.interactable)
				Cart.Sell();
		}

		private void OnCustomerWaiting()
		{
			_waiting = true;

			if (!ProgressSlider.gameObject.activeSelf)
				EnableSellButton();
		}
		
		private void OnSaleStarted()
		{
			_waiting = false;
			DisableSellButton();
			ShowProgressSlider();
		}
		
		private void OnSold()
		{
			if (_waiting)
				EnableSellButton();

			HideProgressSlider();
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
