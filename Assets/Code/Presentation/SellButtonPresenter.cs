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

		[Inject]
		public HotDogCart Cart { private get; set; }

		[Inject]
		public void Initialize()
		{
			Cart.Events
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => SellButton.interactable = false);

			Cart.Events
				.OfType<HotDogCartEvent, HotDogInABunSoldEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => SellButton.interactable = true);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Cart.Sell();
		}
	}
}
