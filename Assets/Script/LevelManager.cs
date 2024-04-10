using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class LevelManager : MonoBehaviour {
	public static LevelManager Instance{ get; private set;}
    public int testLevelMap = 1;
	CameraFollowZS Camera;

    void Awake()
    {
        Instance = this;

        if (FindObjectOfType<LevelWave>())
        {
            Debug.LogError("Notice: There are a Level on this scene!");
            return;
        }
            
        if (GameMode.Instance)
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

	void Start () {
		Camera = FindObjectOfType<CameraFollowZS> ();
	}
}
