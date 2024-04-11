using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class HelicopterUIZS : MonoBehaviour
{
    [FormerlySerializedAs("helicopterLeft")] [SerializeField] private GameObject helicopterLeftT;
    [FormerlySerializedAs("helicopterRight")] [SerializeField] private GameObject helicopterRightT;

    private void Update()
    {
        if (HellicopterFinishPointZS.Instance.isShowing && (Mathf.Abs( HellicopterFinishPointZS.Instance.gameObject.transform.position.x - Camera.main.transform.position.x) > 5))
        {
            helicopterLeftT.SetActive(HellicopterFinishPointZS.Instance.gameObject.transform.position.x < GameManagerZS.Instance.player.transform.position.x);
            helicopterRightT.SetActive(HellicopterFinishPointZS.Instance.gameObject.transform.position.x > GameManagerZS.Instance.player.transform.position.x);
        }
        else
        {
            helicopterLeftT.SetActive(false);
            helicopterRightT.SetActive(false);
        }
    }
}
