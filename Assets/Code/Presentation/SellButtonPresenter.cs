using Assets.Code.Model.Selling;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Code.Presentation
{
	public class SellButtonPresenter : MonoBehaviour, IPointerClickHandler
	{
		[Inject]
		public HotDogCart Cart { get; private set; }

		public void OnPointerClick(PointerEventData eventData)
		{
			Cart.Sell();
		}
	}
}
