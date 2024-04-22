using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopCharacterUpgradeZS : MonoBehaviour
{
    [FormerlySerializedAs("gunID")] public GunTypeIDZS gunIdzs;
    [FormerlySerializedAs("currentRangeDamage")]
    [Space]
    [SerializeField] private TextMeshProUGUI currentRangeDamageE;

    [FormerlySerializedAs("upgradeRangeDamageStep")]
    [Space]
    [SerializeField] private TextMeshProUGUI upgradeRangeDamageStepE;

    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private GameObject lockedObj;
    [SerializeField] private TextMeshProUGUI unlockPriceTxt;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject dotHoderR;
    
    private List<Image> upgradeDotsS;

    public Sprite dotImageOn, dotImageOff;

    private bool isMaxX = false;
    
    private void Start()
    {
        upgradeDotsS = new List<Image>();
        upgradeDotsS.Add(dot.GetComponent<Image>());
        for (int i = 1; i < gunIdzs.upgradeSteps.Length; i++)
        {
            upgradeDotsS.Add(Instantiate(dot, dotHoderR.transform).GetComponent<Image>());
        }
       
        if (gunIdzs.CurrentUpgrade + 1 >= gunIdzs.upgradeSteps.Length)
            isMaxX = true;

        UpdateParameterR();
    }

    private void UpdateParameterR()
    {
        lockedObj.SetActive(!gunIdzs.IsUnlocked);
        unlockPriceTxt.text = "$" + gunIdzs.unlockPriceE;

        currentRangeDamageE.text = gunIdzs.UpgradeRangeDamage + "";
        if (isMaxX)
        {
            upgradeRangeDamageStepE.enabled = false;
            price.text = "MAX";
        }

        else
        {
            price.text = gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price + "";
            upgradeRangeDamageStepE.text = "-> " + gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].damage;
        }
       
        SetDotsS(gunIdzs.CurrentUpgrade + 1);
    }

    private void SetDotsS(int number)
    {
        for (int i = 0; i < upgradeDotsS.Count; i++)
        {
            if (i < number)
                upgradeDotsS[i].sprite = dotImageOn;
            else
                upgradeDotsS[i].sprite = dotImageOff;
        }
    }

    public void Upgrade()
    {
        if (isMaxX)
            return;

        if (GlobalValueZS.SavedCoins >= gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price)
        {
            GlobalValueZS.SavedCoins -= gunIdzs.upgradeSteps[gunIdzs.CurrentUpgrade + 1].price;
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundUpgrade);

            gunIdzs.UpgradeCharacterR();


            if (gunIdzs.CurrentUpgrade + 1 >= gunIdzs.upgradeSteps.Length)
                isMaxX = true;

            UpdateParameterR();
        }
        else
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundNotEnoughCoin);
    }

    public void UnlockPrice()
    {
        if(GlobalValueZS.SavedCoins >= gunIdzs.unlockPriceE)
        {
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundUnlockGun);
            GlobalValueZS.SavedCoins -= gunIdzs.unlockPriceE;
            gunIdzs.IsUnlocked = true;
            UpdateParameterR();
        }
    }
}
