using System;
using Assets.Code.Model.Selling.Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Time = Assets.Code.Model.Selling.Time;

namespace Assets.Code.Presentation
{
	public class TimeTextPresenter : MonoBehaviour
	{
		public Text TimeText;

		public DateTime StartDate;

		[Inject]
		public void Initialize(Time time)
		{
			time.Events
				.OfType<TimeEvent, TimeProgressedEvent>()
				.Select(e => (StartDate + e.Duration).ToString("HH:mm:ss tt"))
				.Subscribe(currentTime => TimeText.text = currentTime);
		}
	}
}