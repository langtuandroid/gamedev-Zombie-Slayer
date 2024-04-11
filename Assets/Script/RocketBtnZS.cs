using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RocketBtnZS : MonoBehaviour
{
    [SerializeField] private Text priceTxt;
    private int priceE = 0;

    [Inject] private GameModeZS gameModeZs;
    
    private void Start()
    {
        if (gameModeZs)
            priceE = gameModeZs.rocketPrice;
        priceTxt.text = "$" + priceE.ToString();
    }
    
    public void FireRocket()
    {
        if (GlobalValueZS.SavedCoins >= priceE)
        {
            RocketManager.Instance.FireRocket();
            GlobalValueZS.SavedCoins -= priceE;
        }
        else
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundNotEnoughCoin);
    }
}
