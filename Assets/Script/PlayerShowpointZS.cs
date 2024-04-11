using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class PlayerShowpointZS : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        GameManagerZS.Instance.player.transform.position = transform.position;
    }
}
