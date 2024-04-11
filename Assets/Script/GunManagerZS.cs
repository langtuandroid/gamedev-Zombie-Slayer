using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GunManagerZS : MonoBehaviour
{
    public static GunManagerZS Instance;
    [FormerlySerializedAs("listGun")] [SerializeField] private List<GunTypeIDZS> listGunN;
    
    [FormerlySerializedAs("listGunPicked")] [ReadOnly]  [SerializeField] private List<GunTypeIDZS> listGunPickedD;

    private int currentPosS = 0;
    
    [Inject] private GameModeZS gameModeZs;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            if (gameModeZs)
            {
                foreach (var gun in listGunN)
                {
                    if (GlobalValueZS.isPicked(gun))
                    {
                        AddGunN(gun);
                    }
                }
            }
            else
            {
                for (int i = 0; i < listGunN.Count; i++)
                {
                    AddGunN(listGunN[i]);
                }
            }

            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetPlayerCarryGunN()
    {
        listGunPickedD.Clear();
        foreach (var gun in listGunN)
        {
            if (GlobalValueZS.isPicked(gun))
            {
                AddGunN(gun);
            }
        }
        currentPosS = 0;
    }

    public void AddBulletT(int amount)
    {
        foreach (var gun in listGunPickedD)
        {
            gun.Bullet += amount;
        }
    }

    public void ResetGunBullet()
    {
        foreach (var gun in listGunPickedD)
        {
            gun.ResetBulletT();
        }
    }

    public void AddGunN(GunTypeIDZS gunIdzs, bool pickImmediately = false)
    {
        listGunPickedD.Add(gunIdzs);
    }

    public void SetNewGunDuringGameplay(GunTypeIDZS gunIdzs)
    {
        GunTypeIDZS pickGun = null;
        foreach (var gun in listGunN)
        {
            if (gun.gunID == gunIdzs.gunID)
            {
                if (!listGunPickedD.Contains(gun))
                    AddGunN(gun);
                else
                {
                    foreach (var _gun in listGunPickedD)
                    {
                        if (_gun.gunID == gun.gunID)
                            _gun.ResetBulletT();
                    }
                }

                pickGun = gun;
            }
        }

        if (pickGun != null)
        {
            NextGunN(pickGun);
            pickGun.ResetBulletT();
        }
    }

    public void RemoveGunN(GunTypeIDZS gunIdzs)
    {
        listGunPickedD.Remove(gunIdzs);
    }

    public void NextGunN()
    {
        currentPosS++;
        if(currentPosS>= listGunPickedD.Count)
        {
            currentPosS = 0;
        }

        GameManagerZS.Instance.player.SetGun(listGunPickedD[currentPosS]);
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.swapGun);
    }

    public void NextGunN(GunTypeIDZS gunIdzs)
    {
        if (listGunPickedD[currentPosS].gunID == gunIdzs.gunID)
            return;     //don't swap gun when the player holding the same gun

        for(int i = 0; i < listGunPickedD.Count; i++)
        {
            if(listGunPickedD[i].gunID == gunIdzs.gunID)
            {
                currentPosS = i;
                GameManagerZS.Instance.player.SetGun(listGunPickedD[currentPosS]);
                SoundManagerZS.PlaySfx(SoundManagerZS.Instance.swapGun);
            }
        }
    }

    public GunTypeIDZS GetGunID()
    {
        return listGunPickedD[currentPosS];
    }
}
