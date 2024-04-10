using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RocketBtn : MonoBehaviour
{
    public Text priceTxt;
    int price = 0;

    private void Start()
    {
        if (GameMode.Instance)
            price = GameMode.Instance.rocketPrice;
        priceTxt.text = "$" + price.ToString();
    }
    public void FireRocket()
    {
        if (GlobalValueZS.SavedCoins >= price)
        {
            RocketManager.Instance.FireRocket();
            GlobalValueZS.SavedCoins -= price;
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }
}
