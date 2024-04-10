using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeBtn : MonoBehaviour
{
    public Text priceTxt;

    int price = 0;

    private void Start()
    {
        if (GameMode.Instance)
            price = GameMode.Instance.grenadePrice;

        priceTxt.text = "$" + price.ToString();
    }

    public void ThrowGrenade()
    {
        if (GlobalValueZS.SavedCoins >= price)
        {
            GameManagerZS.Instance.player.ThrowGrenade();
            GlobalValueZS.SavedCoins -= price;
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}
