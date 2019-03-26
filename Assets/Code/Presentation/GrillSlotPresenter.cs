using System;
using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.Presentation
{
	public class GrillSlotPresenter : MonoBehaviour
	{
		public int Index;

		public Slider ProgressSlider;

		public Text SlotText;

		[Inject]
		public Grill Grill { private get; set; }

		[Inject]
		public void Initialize()
		{
			ShowEmptySlot();

			GrillEvents<HotDogAddedEvent>()
				.Where(e => e.Index == Index)
				.Subscribe(_ => SlotText.text = "Cooking");

			GrillEvents<CookingProgressedEvent>()
				.Where(e => e.Index == Index)
				.Subscribe(e => ProgressSlider.value = e.Progress);

			GrillEvents<HotDogCookedEvent>()
				.Where(e => e.Index == Index)
				.Subscribe(_ => SlotText.text = "Cooked");

			GrillEvents<CookedHotDogRemovedEvent>()
				.Where(e => e.Index == Index)
				.Subscribe(_ => ShowEmptySlot());
		}

		private void ShowEmptySlot()
		{
			SlotText.text = "Empty";
			ProgressSlider.value = 0;
		}

		private IObservable<TEvent> GrillEvents<TEvent>() where TEvent : GrillEvent
			=> Grill.Events
				.OfType<GrillEvent, TEvent>()
				.TakeUntilDestroy(gameObject);
	}
}