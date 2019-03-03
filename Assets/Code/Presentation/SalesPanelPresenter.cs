using Assets.Code.Model.Selling;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Assets.Code.Presentation
{
	public class SalesPanelPresenter : MonoBehaviour
	{
		public Text SalesText;

		[Inject]
		public void Initialize(HotDogCart cart)
		{
			cart.Events
				.OfType<HotDogCartEvent, HotDogSoldEvent>()
				.Subscribe(e => SalesText.text += "Hot Dog Sold\n");
		}
	}
}