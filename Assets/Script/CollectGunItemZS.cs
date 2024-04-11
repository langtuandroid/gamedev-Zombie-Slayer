using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectGunItemZS : MonoBehaviour, ICanCollect
{
    [FormerlySerializedAs("gunTypeID")] public GunTypeIDZS gunTypeIdzs;
    [SerializeField] private AudioClip soundCollect;

    public void CollectT()
    {
        SoundManagerZS.PlaySfx(soundCollect);
        GunManagerZS.Instance.SetNewGunDuringGameplay(gunTypeIdzs);
        Destroy(gameObject);
    }
}
