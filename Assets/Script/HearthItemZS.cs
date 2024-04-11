using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class HearthItemZS : MonoBehaviour, ICanCollect
{
    [SerializeField] private int amount = 30;
    [SerializeField] private AudioClip sound;

    public void CollectT()
    {
        GameManagerZS.Instance.player.AddHearthH(amount);
        SoundManagerZS.PlaySfx(sound);
        Destroy(gameObject);
    }
}
