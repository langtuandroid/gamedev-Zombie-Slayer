using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

public class ShopItemRewardZS : MonoBehaviour {
    [SerializeField] private string itemNameE = "ITEM NAME";

    private enum ItemType{DoubleArrow, Posion, Freeze}
	[SerializeField] private ItemType itemType;

	[FormerlySerializedAs("rewardedUnit")] public int rewardedUnitT = 1;

    [SerializeField] private Text nameTxtT;
    [SerializeField] private Text rewardedAmountTxtT;
    [SerializeField] private Text currentAmountTxtT;

    [FormerlySerializedAs("coinPrice")] [ReadOnly] public int coinPriceE = 1;
	[FormerlySerializedAs("coinTxt")] public Text coinTxtT;

    private void OnEnable(){
		UpdateAmountT ();
	}

	private void Start(){
        if (GameMode.Instance)
        {
    
        }
        UpdateAmountT ();

		rewardedAmountTxtT.text = "x" + rewardedUnitT;
		coinTxtT.text = coinPriceE.ToString ();
        nameTxtT.text = itemNameE;
	}

	public void UseCoin(){
		var coins = GlobalValueZS.SavedCoins;
        if (coins >= coinPriceE)
        {
            coins -= coinPriceE;
            GlobalValueZS.SavedCoins = coins;

            DoRewardD();
        }
        else
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
	        //if (AdsManager.Instance && AdsManager.Instance.isRewardedAdReady())
                NotEnoughCoins.Instance.ShowUp();
        }
	}

	private void DoRewardD(){
        switch (itemType)
        {
            case ItemType.DoubleArrow:
                GlobalValueZS.ItemDoubleArrow += rewardedUnitT;
                break;
            case ItemType.Posion:
                GlobalValueZS.ItemPoison += rewardedUnitT;
                break;
            case ItemType.Freeze:
                GlobalValueZS.ItemFreeze += rewardedUnitT;
                break;
            default:
                break;
        }

		UpdateAmountT ();
        SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);
	}

    private void UpdateAmountT()
    {
        switch (itemType)
        {
            case ItemType.DoubleArrow:
                currentAmountTxtT.text = "current: " + GlobalValueZS.ItemDoubleArrow;
                break;
            case ItemType.Posion:
                currentAmountTxtT.text = "current: " + GlobalValueZS.ItemPoison;
                break;
            case ItemType.Freeze:
                currentAmountTxtT.text = "current: " + GlobalValueZS.ItemFreeze;
                break;
            default:
                break;
        }
    }
}
