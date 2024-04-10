using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChooser : MonoBehaviour
{
    public static WeaponChooser Instance;
    public Image gunTypeA, gunTypeB;
    public GunTypeIDZS[] listGunA;
    public GunTypeIDZS[] listGunB;

    private void Awake()
    {
        Instance = this;
    }

    //private void OnEnable()
    //{
    //    CheckGun();
    //}

    //private void Start()
    //{
    //    CheckGun();
    //}

    private void Update()
    {
        CheckGun();
    }

    bool hasGunA = false;
    bool hasGunB = false;
    void CheckGun()
    {
        if (!hasGunA)
        {
            foreach (var gunA in listGunA)
            {
                if (GlobalValueZS.isPicked(gunA))
                {
                    hasGunA = true;
                    gunTypeA.sprite = gunA.icon;
                }
            }

            if (!hasGunA)
            {
                gunTypeA.sprite = listGunA[0].icon;
                GlobalValueZS.pickGun(listGunA[0]);
            }
        }

        if (!hasGunB)
        {
            foreach (var gunB in listGunB)
            {
                if (GlobalValueZS.isPicked(gunB))
                {
                    hasGunB = true;
                    gunTypeB.sprite = gunB.icon;
                }
            }

            if (!hasGunB)
            {
                foreach (var gunB in listGunB)
                {
                    if (gunB.IsUnlocked)
                    {
                        gunTypeB.sprite = gunB.icon;
                        GlobalValueZS.pickGun(gunB);

                        if (GunManagerZS.Instance)
                            GunManagerZS.Instance.ResetPlayerCarryGunN();      //update the gun list if back to HomeScene from Playing scene
                    }
                }
            }
        }
    }

    public void SetGun(GunTypeIDZS gunIdzs)
    {
        if (gunIdzs.gunType == GUNTYPE.typeA)
        {
            gunTypeA.sprite = gunIdzs.icon;
        }
        else
        {
            gunTypeB.sprite = gunIdzs.icon;
        }
    }

    public void PlayGame()
    {
        MainMenuHomeScene.Instance.LoadScene();
    }
}