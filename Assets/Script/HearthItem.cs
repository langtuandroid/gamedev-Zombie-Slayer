using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class HearthItem : MonoBehaviour, ICanCollect
{
    public int amount = 30;
    public AudioClip sound;

    public void CollectT()
    {
        GameManagerZS.Instance.player.AddHearth(amount);
        SoundManager.PlaySfx(sound);
        Destroy(gameObject);
    }
}
