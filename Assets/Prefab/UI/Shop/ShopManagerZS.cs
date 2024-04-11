using UnityEngine;
using UnityEngine.UI;

namespace Prefab.UI.Shop
{
	public class ShopManagerZS : MonoBehaviour {

		[SerializeField] private GameObject[] shopPanelsS;
		[SerializeField] private Sprite buttonActiveImageE, buttonInActiveImageE;
		public Image upgradeButton, upgradeWallButton, buyCoinButton;

		private void Start () {
			DisableObj();
			ActivePanel (shopPanelsS[0]);
			SetActiveBut(0);
		}

		private void DisableObj(){
			foreach (var obj in shopPanelsS) {
				obj.SetActive(false);
			}
		}

		private void ActivePanel(GameObject obj){
			//		StartCoroutine (
			//			MMFade.FadeCanvasGroup (canv, 0.5f, 1));

			obj.SetActive(true);
		}
	
		public void SwichPanel(GameObject obj){
			for (int i = 0; i < shopPanelsS.Length; i++) {
				if (obj == shopPanelsS[i]) {
					DisableObj();
					ActivePanel (shopPanelsS[i]);
					SetActiveBut(i);

					break;
				}
			}
			SoundManagerZS.Click ();
		}

		private void SetActiveBut(int i)
		{
			upgradeButton.sprite = buttonInActiveImageE;
			upgradeWallButton.sprite = buttonInActiveImageE;
			buyCoinButton.sprite = buttonInActiveImageE;

			switch (i)
			{
				case 0:
					upgradeButton.sprite = buttonActiveImageE;
					break;
				case 1:
					upgradeWallButton.sprite = buttonActiveImageE;
					break;
				case 2:
					buyCoinButton.sprite = buttonActiveImageE;
					break;
			}
		}
	}
}
