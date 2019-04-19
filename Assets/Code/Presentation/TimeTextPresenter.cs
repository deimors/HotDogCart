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
		
		[Inject]
		public void Initialize(Time time)
		{
			time.Events
				.OfType<TimeEvent, TimeProgressedEvent>()
				.Select(e => e.CurrentTime.ToString("HH:mm tt"))
				.Subscribe(currentTime => TimeText.text = currentTime);
		}
	}
}