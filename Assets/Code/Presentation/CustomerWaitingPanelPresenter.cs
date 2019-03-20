using System;
using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
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
		public Customers Customers { private get; set; }
		
		[Inject]
		public void Initialize()
		{
			ClearCustomerWaiting();

			OnCustomersEvent<LineLengthIncreasedEvent>(e => ShowCustomerWaiting(e.LineLength));

			OnCustomersEvent<LineLengthDecreasedEvent>(e => ShowCustomerWaiting(e.LineLength));

			OnCustomersEvent<LineEmptyEvent>(_ => ClearCustomerWaiting());

			OnCustomersEvent<MissedCustomerEvent>(_ => ShowMissedCustomer());
		}

		private void OnCustomersEvent<TEvent>(Action<TEvent> action)
			=> Customers.Events
				.OfType<CustomersEvent, TEvent>()
				.TakeUntilDestroy(gameObject)
				.Subscribe(action);


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