using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class HandDirectionUI : MonoBehaviour
{
    public GameObject leftHand, rightHand;

    void OnEnable()
    {
        if (GameManagerZS.Instance && HellicopterFinishPoint.Instance)
        {
            leftHand.SetActive(HellicopterFinishPoint.Instance.gameObject.transform.position.x < GameManagerZS.Instance.player.transform.position.x);
            rightHand.SetActive(HellicopterFinishPoint.Instance.gameObject.transform.position.x > GameManagerZS.Instance.player.transform.position.x);
        }
    }
}
