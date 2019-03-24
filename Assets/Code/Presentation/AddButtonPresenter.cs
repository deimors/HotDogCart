using Assets.Code.Model.Selling;
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
		}
	}
}