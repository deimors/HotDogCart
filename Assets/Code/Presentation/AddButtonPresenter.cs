using Assets.Code.Model.Selling;
using Assets.Code.Model.Selling.Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.Presentation
{
	public class AddButtonPresenter : MonoBehaviour
	{
		public Button AddButton;

		[Inject]
		public Grill Grill { private get; set; }

		[Inject]
		public void Initialize()
		{
			AddButton.onClick.AddListener(() => Grill.AddHotDog());

			var enableStream = Grill.Events.OfType<GrillEvent, CanAddHotDogEvent>().Select(_ => true);
			var disableStream = Grill.Events.OfType<GrillEvent, CantAddHotDogEvent>().Select(_ => false);

			enableStream
				.Merge(disableStream)
				.TakeUntilDestroy(gameObject)
				.Subscribe(e => AddButton.interactable = e);
		}
	}
}