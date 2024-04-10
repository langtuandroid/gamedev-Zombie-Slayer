using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class UI_UI : MonoBehaviour
{
    [Header("GUN UI")]
    public Image gunIcon;
    public Text bulletLeft;
    [Space]
    public Text coinTxt;

    private void Update()
    {
        //healthSlider.value = Mathf.Lerp(healthSlider.value, healthValue, lerpSpeed * Time.deltaTime);
        
        coinTxt.text = GlobalValueZS.SavedCoins + "";
        bulletLeft.text = GameManagerZS.Instance.player.gunTypeIdzs.Bullet + "";
        gunIcon.sprite = GameManagerZS.Instance.player.gunTypeIdzs.icon;
    }

    public void NextGun()
    {
        GunManagerZS.Instance.NextGunN();
    }
   
}
