using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinItemZS : MonoBehaviour, ICanCollect
{
    [ReadOnly] [SerializeField] private int rewardedD = 5;
    [SerializeField] private AudioClip soundD;

    public void Init(int rewarded)
    {
        rewardedD = rewarded;
    }

    public void CollectT()
    {
        GlobalValueZS.SavedCoins += rewardedD;
        SoundManager.PlaySfx(soundD);
        FloatingTextManager.Instance.ShowText("+" + rewardedD, transform.position, Vector2.zero, Color.yellow);
        gameObject.SetActive(false);
    }
}
