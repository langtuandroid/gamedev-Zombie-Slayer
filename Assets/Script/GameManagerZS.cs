using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script
{
	public class GameManagerZS: MonoBehaviour {
		public static GameManagerZS Instance{ get; private set;}
		public bool IsWatchingAd { get; set; }


		public enum GameState{Menu,Playing, GameOver, Success, Pause};
		[FormerlySerializedAs("State")] [ReadOnly] public GameState state;
    
		[ReadOnly] public int levelStarGot;
		public LayerMask layerGround, layerEnemy, layerPlayer;
		[HideInInspector]
		public List<IListener> Listeners;

		[FormerlySerializedAs("Player")] [ReadOnly] public PlayerController player;
		
		public void AddListener(IListener listener)
		{
			if (!Listeners.Contains(listener))    
				Listeners.Add(listener);
		}
		
		public void RemoveListener(IListener listener)
		{
			if (Listeners.Contains(listener))     
				Listeners.Remove(listener);
		}

		private void Awake(){
			Instance = this;
			state = GameState.Menu;
			Listeners = new List<IListener> ();

		}
		
		private IEnumerator Start(){
			yield return new WaitForEndOfFrame ();

		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
			{
				PlayerPrefs.DeleteAll();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
        
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
			{
				bool isRemoveAd = GlobalValueZS.RemoveAds;

				PlayerPrefs.DeleteAll();
				GlobalValueZS.RemoveAds = isRemoveAd;

				GlobalValueZS.LevelPass = 9999;
				SceneManager.LoadScene(0);
			}
		}
    
		public void StartGameE(){
			state = GameState.Playing;

			//Get all objects that have IListener
			var listener_ = FindObjectsOfType<MonoBehaviour>().OfType<IListener>();
			foreach (var _listener in listener_) {
				Listeners.Add (_listener);
			}

			foreach (var item in Listeners) {
				item.IPlayY ();
			}
		}

		public void GamePause(){
			state = GameState.Pause;
			foreach (var item in Listeners)
				item.IPauseE ();
		}

		public void UnPause(){
			state = GameState.Playing;
			foreach (var item in Listeners)
				item.IUnPauseE ();
		}

		public void VictoryY(){
			Time.timeScale = 1;
			SoundManagerZS.Instance.PauseMusicC(true);
			state = GameState.Success;

			foreach (var item in Listeners)
			{
				if (item != null) 
					item.ISuccessS();
			}
        
			//save level and save star
			if (GlobalValueZS.LevelPlaying > GlobalValueZS.LevelPass)
				GlobalValueZS.LevelPass = GlobalValueZS.LevelPlaying;
		}
   
		private void OnDisable()
		{
			GunManagerZS.Instance.ResetPlayerCarryGunN();
		}

		public void UnlockNextLevel()
		{
			//if (GlobalValue.levelPlaying > GlobalValue.LevelPass)
			//    GlobalValue.LevelPass = GlobalValue.levelPlaying;
		}

		public void GameOver(){
			Time.timeScale = 1;
			SoundManagerZS.Instance.PauseMusicC(true);
      
			if (state == GameState.GameOver)
				return;
		
			state = GameState.GameOver;

			foreach (var item in Listeners)
				item.IGameOverR ();
		}

		[HideInInspector]
		private List<GameObject> enemyAlivesS;
		[HideInInspector]
		private List<GameObject> listEnemyChasingPlayerR;

		public void RegisterEnemy(GameObject obj)
		{
			enemyAlivesS.Add(obj);
		}

		public void RemoveEnemy(GameObject obj)
		{
			enemyAlivesS.Remove(obj);
		}

		public int EnemyAlive()
		{
			return enemyAlivesS.Count;
		}
	}
}
