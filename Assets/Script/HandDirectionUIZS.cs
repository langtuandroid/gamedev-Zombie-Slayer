using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class HandDirectionUIZS : MonoBehaviour
{
    [SerializeField] private GameObject leftHandD;
    [SerializeField] private GameObject rightHandD;

    private void OnEnable()
    {
        if (GameManagerZS.Instance && HellicopterFinishPointZS.Instance)
        {
            leftHandD.SetActive(HellicopterFinishPointZS.Instance.gameObject.transform.position.x < GameManagerZS.Instance.player.transform.position.x);
            rightHandD.SetActive(HellicopterFinishPointZS.Instance.gameObject.transform.position.x > GameManagerZS.Instance.player.transform.position.x);
        }
    }
}
