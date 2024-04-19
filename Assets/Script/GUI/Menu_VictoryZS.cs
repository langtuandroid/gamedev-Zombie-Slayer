using System.Collections;
using UnityEngine;

namespace Script.GUI
{
	/// <summary>
	/// Handle Level Complete UI of Menu object
	/// </summary>
	public class Menu_VictoryZS : MonoBehaviour {
		[SerializeField] private GameObject menu;
		[SerializeField] private GameObject restart;
		[SerializeField] private GameObject next;

		private void Awake(){
			menu.SetActive (false);
			restart.SetActive (false);
			next.SetActive (false);
		}

		private IEnumerator Start()
		{
			SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundVictoryPanel);
        
			GlobalValueZS.LevelStar(GlobalValueZS.LevelPlaying, GameManagerZS.Instance.levelStarGot);
			yield return new WaitForSeconds(0.5f);

			menu.SetActive(true);
			restart.SetActive(true);
        
			next.SetActive(LevelWaveZS.Instance);
		}
	}
}
