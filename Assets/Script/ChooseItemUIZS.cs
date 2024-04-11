using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class ChooseItemUIZS : MonoBehaviour
{
    [FormerlySerializedAs("icon")] [SerializeField] private Image iconN;
    [FormerlySerializedAs("gunID")] [SerializeField] private  GunTypeIDZS gunIDD;
    [FormerlySerializedAs("unlockBtn")] [SerializeField] private  GameObject unlockButton;
    [FormerlySerializedAs("pick")] [SerializeField] private  Image pickK;

    [Inject] private MainMenuHomeSceneZS mainMenuHomeSceneZs;
    
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
        mainMenuHomeSceneZs.OpenShop(true);
    }

    public void SetGun()
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.chooseGun);
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
