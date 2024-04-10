using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopCharacterUpgrade : MonoBehaviour
{
    [FormerlySerializedAs("gunID")] public GunTypeIDZS gunIdzs;
    [Space]
    public Text
        currentRangeDamage, upgradeRangeDamageStep;

    public Text price;
    public GameObject lockedObj;
    public Text unlockPriceTxt;
    public GameObject dot;
    public GameObject dotHoder;
    List<Image> upgradeDots;

    public Sprite dotImageOn, dotImageOff;

    bool isMax = false;

    // Start is called before the first frame update
    void Start()
    {
        upgradeDots = new List<Image>();
        upgradeDots.Add(dot.GetComponent<Image>());
        for (int i = 1; i < gunIdzs.upgradeSteps.Length; i++)
        {
            upgradeDots.Add(Instantiate(dot, dotHoder.transform).GetComponent<Image>());
        }
       
        if (gunIdzs.CurrentUpgrade + 1 >= gunIdzs.upgradeSteps.Length)
            isMax = true;

        UpdateParameter();
    }

    void UpdateParameter()
    {
        lockedObj.SetActive(!gunIdzs.IsUnlocked);
        unlockPriceTxt.text = "$" + gunIdzs.unlockPrice;

        currentRangeDamage.text = gunIdzs.UpgradeRangeDamage + "";
        if (isMax)
        {
            upgradeRangeDamageStep.enabled = false;
            price.text = "MAX";
        }

        else
        {
            price.text = gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price + "";
            upgradeRangeDamageStep.text = "-> " + gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].damage;
        }
       
        SetDots(gunIdzs.CurrentUpgrade + 1);
    }

    void SetDots(int number)
    {
        for (int i = 0; i < upgradeDots.Count; i++)
        {
            if (i < number)
                upgradeDots[i].sprite = dotImageOn;
            else
                upgradeDots[i].sprite = dotImageOff;
        }
    }

    public void Upgrade()
    {
        if (isMax)
            return;

        if (GlobalValueZS.SavedCoins >= gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price)
        {
            GlobalValueZS.SavedCoins -= gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price;
            SoundManager.PlaySfx(SoundManager.Instance.soundUpgrade);

            gunIdzs.UpgradeCharacterR();


            if (gunIdzs.CurrentUpgrade + 1 >= gunIdzs.upgradeSteps.Length)
                isMax = true;

            UpdateParameter();
        }
        else
            SoundManager.PlaySfx(SoundManager.Instance.soundNotEnoughCoin);
    }

    public void UnlockPrice()
    {
        if(GlobalValueZS.SavedCoins >= gunIdzs.unlockPrice)
        {
            SoundManager.PlaySfx(SoundManager.Instance.soundUnlockGun);
            GlobalValueZS.SavedCoins -= gunIdzs.unlockPrice;
            gunIdzs.IsUnlocked = true;
            UpdateParameter();
        }
    }
}
