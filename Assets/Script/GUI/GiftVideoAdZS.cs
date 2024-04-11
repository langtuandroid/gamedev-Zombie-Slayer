using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GiftVideoAdZS : MonoBehaviour
{
    public Text rewardedTxtT;
    public GameObject buttonN;
    private bool allowShow = true;
 
    public void WatchVideoAd()
    {
        SoundManagerZS.Click();
        allowShow = false;
        Invoke(nameof(AllowShowW), 2);
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        if (isSuccess)
        {
            GlobalValueZS.SavedCoins += rewarded;
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundPurchased);
        }
    }

    private void AllowShowW()
    {
        allowShow = true;
    }
}
