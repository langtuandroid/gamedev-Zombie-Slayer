using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelWaveZS : MonoBehaviour
{
    public static LevelWaveZS Instance;
    [FormerlySerializedAs("mission")] [Header("------MISSION------")] public Mission missionN;
    [FormerlySerializedAs("missionInformation")] public string missionInformationN = "";

    private void Awake()
    {
        Instance = this;
    }

    public Mission CurrentMissionN()
    {
        return missionN;
    }
}

public enum MissionType { Normal, Survior, DefenseFence, ProtectMan}
public enum ShowHelicopter { OnStart, Timer}
[System.Serializable]
public class Mission
{
    public MissionType missionType;
    [Header("------SHOW HELICOPTER------")]
    public ShowHelicopter showHelicopter;
    public float timer = 180;
}