using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class BulletPackZS : MonoBehaviour, ICanCollect
{
    [SerializeField] private int amount = 30;
    [SerializeField] private AudioClip sound;

    public void CollectT()
    {
        GunManagerZS.Instance.AddBulletT(amount);
        SoundManagerZS.PlaySfx(sound);
        Destroy(gameObject);
    }
}
