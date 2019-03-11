using System;
using Assets.Code.Model.Selling;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.Presentation
{
	public class CustomerWaitingPanelPresenter : MonoBehaviour
	{
		public Image BackgroundImage;
		public Text CustomerText;
		
		[Inject]
		public void Initialize(HotDogCart cart, Customers customers)
		{
			ClearCustomerWaiting();

			customers.Events
				.OfType<CustomersEvent, CustomerStartedWaitingEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => ShowCustomerWaiting());

			cart.Events
				.OfType<HotDogCartEvent, SaleStartedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => ClearCustomerWaiting());

			customers.Events
				.OfType<CustomersEvent, PotentialCustomerWalkedAwayEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => ShowMissedCustomer());
		}


		private void ShowCustomerWaiting()
		{
			BackgroundImage.color = Color.green;
			CustomerText.text = "Waiting";
		}

		private void ClearCustomerWaiting()
		{
			BackgroundImage.color = Color.white;
			CustomerText.text = "Empty";
		}

		private void ShowMissedCustomer()
		{
			var origColor = BackgroundImage.color;
			var origText = CustomerText.text;

			BackgroundImage.color = Color.red;
			CustomerText.text = "Missed";

			Observable.Timer(TimeSpan.FromSeconds(0.5))
				.Where(_ => BackgroundImage.color == Color.red)
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ =>
				{
					BackgroundImage.color = origColor;
					CustomerText.text = origText;
				});
		}
	}
}