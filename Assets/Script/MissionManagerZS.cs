using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MissionManagerZS : MonoBehaviour
{
    [SerializeField] private  Text missionTxt;
    [SerializeField] private  Text missionInformation;
    [SerializeField] private  Text timerTxt;
    [FormerlySerializedAs("TimerObj")] [SerializeField] private  GameObject timerObj;
    private Mission currentMissionN;
    private void Start()
    {
        currentMissionN = LevelWaveZS.Instance.CurrentMissionN();

        if(LevelWaveZS.Instance.missionN.showHelicopter == ShowHelicopter.Timer)
        {
            StartCoroutine(TimerCoc(currentMissionN.timer));
        }else if (LevelWaveZS.Instance.missionN.showHelicopter == ShowHelicopter.OnStart)
        {
            timerObj.SetActive(false);
        }

        if (currentMissionN.missionType == MissionType.DefenseFence)
            missionTxt.text = "Defense the fence!";
        else if (currentMissionN.missionType == MissionType.Survior)
            missionTxt.text = "Hold on and fight!";
        else if (currentMissionN.missionType == MissionType.Normal)
            missionTxt.text = "Reach to the helicopter!";
        else if (currentMissionN.missionType == MissionType.ProtectMan)
            missionTxt.text = "Rescue the babe";
        

        if (LevelWaveZS.Instance.missionInformationN == "")
        {
            if (currentMissionN.missionType == MissionType.DefenseFence)
                missionInformation.text = "Defense the fence!";
            else if (currentMissionN.missionType == MissionType.Survior)
                missionInformation.text = "Hold on and fight!";
            else if (currentMissionN.missionType == MissionType.Normal)
                missionInformation.text = "Reach to the helicopter!";
            else if (currentMissionN.missionType == MissionType.ProtectMan)
                missionInformation.text = "Rescue the babe";
        }
        else
            missionInformation.text = LevelWaveZS.Instance.missionInformationN;
    }

    private IEnumerator TimerCoc(float timer)
    {
        HellicopterFinishPointZS.Instance.Hide();
        timerTxt.text = timer + "";

        while (timer > 0)
        {
            while (GameManagerZS.Instance.state != GameManagerZS.GameState.Playing)
                yield return null;

            yield return new WaitForSeconds(1);
            timer--;
            timerTxt.text = timer + "";
        }

        HellicopterFinishPointZS.Instance.Show();
    }
}
