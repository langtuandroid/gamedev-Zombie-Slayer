using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class TestFinishLevelBtn : MonoBehaviour
{
    public void FinishLevel()
    {
        GameManagerZS.Instance.VictoryY();
    }
}
