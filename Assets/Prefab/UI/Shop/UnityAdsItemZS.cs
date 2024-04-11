using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityAdsItemZS : MonoBehaviour
{
    public GameObject but;
    public Text rewardedTxt;

    private void Update()
    {
        //but.SetActive(GameMode.Instance && GameMode.Instance.isRewardedAdReady());
       // but.SetActive(AdsManager.Instance && AdsManager.Instance.isRewardedAdReady());
        //if (AdsManager.Instance)
       // {
           // if (!AdsManager.Instance.isRewardedAdReady())
              rewardedTxt.text = "NO AD AVAILABLE NOW!";
          //  else
              //  rewardedTxt.text = "+" + AdsManager.Instance.getRewarded;
       // }else
          //  rewardedTxt.text = "NO AD AVAILABLE NOW!";
    }

    public void WatchVideoAd()
    {
    }

    private void AdsManager_AdResult(bool isSuccess, int rewarded)
    {
        if (isSuccess)
        {
            GlobalValueZS.SavedCoins += rewarded;
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundPurchased);
        }
    }
}
