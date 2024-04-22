using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponChooser : MonoBehaviour
{
    [Header("Slots")] 
    [SerializeField] private Sprite substrateImage;

    [SerializeField] private Sprite emptyImage;
    
    [Space]
    
    [SerializeField] private Image emptyCircle;
    [SerializeField] private TextMeshProUGUI emptyText;

    [Space] 
    
    [SerializeField] private Image emptyCircleTwo;
    [SerializeField] private TextMeshProUGUI emptyTextTwo;
    
    public static WeaponChooser Instance;
    public Image gunTypeA, gunTypeB;
    public GunTypeIDZS[] listGunA;
    public GunTypeIDZS[] listGunB;

    [Inject] private MainMenuHomeSceneZS mainMenuHomeSceneZs;
    
    private void Awake()
    {
        Instance = this;
    }

    //private void OnEnable()
    //{
    //    CheckGun();
    //}

    //private void Start()
    //{
    //    CheckGun();
    //}

    private void Update()
    {
        CheckGun();
    }

    private bool hasGunA = false;
    private bool hasGunB = false;
    
    private void CheckGun()
    {
        if (!hasGunA)
        {
            foreach (var gunA in listGunA)
            {
                if (GlobalValueZS.isPicked(gunA))
                {
                    hasGunA = true;
                    gunTypeA.sprite = gunA.icon;
                    emptyCircle.sprite = substrateImage;
                    emptyText.gameObject.SetActive(false);
                }
            }

            if (!hasGunA)
            {
                gunTypeA.sprite = listGunA[0].icon;
                GlobalValueZS.pickGun(listGunA[0]);
            }
        }

        if (!hasGunB)
        {
            foreach (var gunB in listGunB)
            {
                if (GlobalValueZS.isPicked(gunB))
                {
                    hasGunB = true;
                    gunTypeB.sprite = gunB.icon;
                    emptyCircleTwo.sprite = substrateImage;
                    emptyTextTwo.gameObject.SetActive(false);
                }
            }

            if (!hasGunB)
            {
                foreach (var gunB in listGunB)
                {
                    if (gunB.IsUnlocked)
                    {
                        gunTypeB.sprite = gunB.icon;
                        emptyCircleTwo.sprite = substrateImage;
                        emptyTextTwo.gameObject.SetActive(false);
                        GlobalValueZS.pickGun(gunB);

                        if (GunManagerZS.Instance)
                            GunManagerZS.Instance.ResetPlayerCarryGunN();      //update the gun list if back to HomeScene from Playing scene
                    }
                }
            }
        }

        if (hasGunB == false)
        {
            gunTypeB.gameObject.SetActive(false);
        }
        else
        {
            gunTypeB.gameObject.SetActive(true);
        }
    }

    public void SetGun(GunTypeIDZS gunIdzs)
    {
        if (gunIdzs.gunType == GUNTYPE.typeA)
        {
            gunTypeA.sprite = gunIdzs.icon;
            emptyCircle.sprite = substrateImage;
        }
        else
        {
            gunTypeB.sprite = gunIdzs.icon;
            emptyCircleTwo.sprite = substrateImage;
        }
    }

    public void PlayGame()
    {
        mainMenuHomeSceneZs.LoadScene();
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }
}