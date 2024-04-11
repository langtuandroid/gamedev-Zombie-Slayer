using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;


public class LevelManagerZS : MonoBehaviour {
    private static LevelManagerZS Instance{ get; set;}
    public int testLevelMap = 1;
	private CameraFollowZS cm;

    [Inject] private GameModeZS gameModeZs;
    
    private void Awake()
    {
        Instance = this;

        if (FindObjectOfType<LevelWaveZS>())
        {
            Debug.LogError("Notice: There are a Level on this scene!");
            return;
        }
            
        if (gameModeZs)
        {
            //Instantiate(LevelMaps[GlobalValue.levelPlaying - 1], Vector2.zero, Quaternion.identity);
            var _go = Resources.Load("Levels/Level " + GlobalValueZS.LevelPlaying ) as GameObject;
            if (_go != null)
                Instantiate(_go, Vector2.zero, Quaternion.identity);
            else
                Debug.LogError("NO LEVEL IN RESOURCE: Level " + GlobalValueZS.LevelPlaying);
        }
        else
        {
            Debug.LogError("PLAY GAME CORRECTLY FROM THE LOGO SCENE");
            var _go = Resources.Load("Levels/Level " + testLevelMap) as GameObject;
            if (_go != null)
                Instantiate(_go, Vector2.zero, Quaternion.identity);
            else
                Debug.LogError("NO LEVEL IN RESOURCE: Level " + testLevelMap);
        }
    }

	private void Start () {
		cm = FindObjectOfType<CameraFollowZS> ();
	}
}
