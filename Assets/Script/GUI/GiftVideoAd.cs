using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftVideoAd : MonoBehaviour
{
    public Text rewardedTxt;
    public GameObject button;
    private bool allowShow = true;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void WatchVideoAd()
    {
        SoundManager.Click();
        allowShow = false;
        Invoke(nameof(AllowShow), 2);
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        if (isSuccess)
        {
            GlobalValueZS.SavedCoins += rewarded;
            SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);
        }
    }

    void AllowShow()
    {
        allowShow = true;
    }
}
