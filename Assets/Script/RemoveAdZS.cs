using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RemoveAdZS : MonoBehaviour
{
    public Text priceTxt;
    public Text rewardedTxt;
   
    [Inject] private GameModeZS gameModeZs;
    
    private void Start()
    {
        gameObject.SetActive(gameModeZs && !GlobalValueZS.RemoveAds);
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_PURCHASING
    
#endif
    }

    public void Buy()
    {
#if UNITY_PURCHASING
        SoundManager.Click();
        GameMode.Instance.BuyRemoveAds();
#else
        NoIAPMessageZS.Instance.OpenPanel(true);
#endif
    }

}
