﻿using UnityEngine;
#if UNITY_PURCHASING

#else
public class PurchaserZS : MonoBehaviour {

    private void Start()
    {
		Debug.LogError("Please turn on IAP in Services tab to able use the IAP features");
    }
    public void BuyItem1()
{
Debug.LogError ("You need to turn on IAP in Windown/Services tab to use this feature");
}

public void BuyItem2()
{
Debug.LogError ("You need to turn on IAP in Windown/Services tab to use this feature");
}

public void BuyItem3()
{
Debug.LogError ("You need to turn on IAP in Windown/Services tab to use this feature");
}

public void BuyRemoveAds()
{
Debug.LogError ("You need to turn on IAP in Windown/Services tab to use this feature");
}
} 
#endif