using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class PlayerShowpoint : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        GameManagerZS.Instance.player.transform.position = transform.position;
    }
}
