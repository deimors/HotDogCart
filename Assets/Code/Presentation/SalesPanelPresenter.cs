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
				.Subscribe(_ => SalesText.text += message + "\n");
	}
}