using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPackZS : MonoBehaviour, ICanCollect
{
    [SerializeField] private int amount = 30;
    [SerializeField] private AudioClip sound;

    public void CollectT()
    {
        GunManagerZS.Instance.AddBulletT(amount);
        SoundManager.PlaySfx(sound);
        Destroy(gameObject);
    }
}
