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
		public void Initialize(Customers customers)
		{
			ClearCustomerWaiting();

			customers.Events
				.OfType<CustomersEvent, LineLengthIncreasedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(e => ShowCustomerWaiting(e.LineLength));

			customers.Events
				.OfType<CustomersEvent, LineLengthDecreasedEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(e => ShowCustomerWaiting(e.LineLength));

			customers.Events
				.OfType<CustomersEvent, LineEmptyEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => ClearCustomerWaiting());

			customers.Events
				.OfType<CustomersEvent, MissedCustomerEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(_ => ShowMissedCustomer());
		}


		private void ShowCustomerWaiting(int lineLength)
		{
			BackgroundImage.color = Color.green;
			CustomerText.text = $"{lineLength} Waiting";
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