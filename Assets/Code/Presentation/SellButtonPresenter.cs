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

		[Inject]
		public HotDogCart Cart { private get; set; }

		[Inject]
		public void Initialize()
		{
			HideProgressSlider();

			Cart.Events
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => OnSaleStarted());

			Cart.Events
				.OfType<HotDogCartEvent, HotDogInABunSoldEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => OnSold());

			Cart.Events
				.OfType<HotDogCartEvent, SaleProgressedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(e => ProgressSlider.value = e.Progress);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Cart.Sell();
		}
		
		private void OnSaleStarted()
		{
			DisableSellButton();
			ShowProgressSlider();
		}
		
		private void OnSold()
		{
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
