using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameModeZS))]

public class GameModeEditor : Editor
{
    string message = "MESSAGE";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

       if( GUILayout.Button("RESET DATA"))
        {
            PlayerPrefs.DeleteAll();
            message = "RESETED ALL DATA!";
        }

        if (GUILayout.Button("SET 99999 COINS"))
        {
            GlobalValueZS.SavedCoins = 99999;
            message = "SETTED 9999 COINS!";
        }

        if (GUILayout.Button("UNLOCK ALL LEVELS"))
        {
            GlobalValueZS.LevelPass = 9999;
            message = "UNLOCKED ALL LEVELS";
        }

        GUILayout.Label("MESSAGE: " + message);
    }
}
