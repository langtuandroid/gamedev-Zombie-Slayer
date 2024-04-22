using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GrenadeBtn : MonoBehaviour
{
    public TextMeshProUGUI priceTxt;

    int price = 0;
    
    [Inject] private GameModeZS gameModeZs;

    private void Start()
    {
        if (gameModeZs)
            price = gameModeZs.grenadePrice;

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
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundNotEnoughCoin);
    }
}
