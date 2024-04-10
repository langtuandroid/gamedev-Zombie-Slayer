using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChooseItemUIZS : MonoBehaviour
{
    [FormerlySerializedAs("icon")] [SerializeField] private Image iconN;
    [FormerlySerializedAs("gunID")] [SerializeField] private  GunTypeIDZS gunIDD;
    [FormerlySerializedAs("unlockBtn")] [SerializeField] private  GameObject unlockButton;
    [FormerlySerializedAs("pick")] [SerializeField] private  Image pickK;
    
    private Button ownerButtonZs;

    private void Awake()
    {
        if (gunIDD != null)
        {
            iconN.sprite = gunIDD.icon;
        }

        ownerButtonZs = GetComponent<Button>();
    }

    private void Update()
    {
        ownerButtonZs.interactable = gunIDD.IsUnlocked;
        unlockButton.SetActive(!gunIDD.IsUnlocked);
        pickK.gameObject.SetActive(gunIDD.IsUnlocked);
        pickK.color = GlobalValueZS.isPicked(gunIDD) ? Color.white : Color.black;
    }

    public void OpenShop()
    {
        MainMenuHomeScene.Instance.OpenShop(true);
    }

    public void SetGun()
    {
        SoundManager.PlaySfx(SoundManager.Instance.chooseGun);
        GlobalValueZS.pickGun(gunIDD);

        if(GunManagerZS.Instance)
            GunManagerZS.Instance.ResetPlayerCarryGunN();      //update the gun list if back to HomeScene from Playing scene

        WeaponChooser.Instance.SetGun(gunIDD);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;
        if (gunIDD != null)
        {
            iconN.sprite = gunIDD.icon;
        }
    }
}
