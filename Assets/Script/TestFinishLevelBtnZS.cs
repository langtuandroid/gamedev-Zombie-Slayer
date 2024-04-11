using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class TestFinishLevelBtnZS : MonoBehaviour
{
    public void FinishLevel()
    {
        GameManagerZS.Instance.VictoryY();
    }
}
