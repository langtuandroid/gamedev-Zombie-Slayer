using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NoIAPMessageZS : MonoBehaviour
{
    public static NoIAPMessageZS Instance;
    [SerializeField] private GameObject panelL;
    
    private void Start()
    {
        Instance = this;
        panelL.SetActive(false);
    }

    public void OpenPanel(bool open)
    {
        SoundManagerZS.Click();
        panelL.SetActive(open);
    }
}
