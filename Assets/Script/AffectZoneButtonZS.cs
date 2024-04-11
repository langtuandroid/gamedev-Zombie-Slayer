using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class AffectZoneButtonZS : MonoBehaviour
{
    [SerializeField] public AffectZoneType affectTypeR;
    [SerializeField] private Button ownBtn;

    [Header("COOL DOWN")]
    [SerializeField] private float delayOnStart = 2;
    [SerializeField] private float coolDown = 3f;
    private float coolDownCounterR = 0;
    [SerializeField] private Image image;
    [SerializeField] private Text timerTxt;
    
    private bool allowWorkK = true;
    private bool allowCountingG = false;
    private bool canUseS = true;
    private float holdCounterR = 0;
    
    public CanvasGroup canvasGroup;

    private void Start()
    {
        ownBtn = GetComponent<Button>();
        ownBtn.onClick.AddListener(OnBtnClickK);

        if (image == null)
            image = GetComponent<Image>();
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        allowWorkK = false;
        allowCountingG = true;
        coolDownCounterR = delayOnStart;
    }

    private void Update()
    {
        if (!allowWorkK)
        {
            if (allowCountingG)
            {
                coolDownCounterR -= Time.deltaTime;

                if (coolDownCounterR <= 0)
                    allowWorkK = true;
            }
        }
        else
        {
            holdCounterR -= Time.deltaTime;
        }

        image.fillAmount = Mathf.Clamp01((coolDown - coolDownCounterR) / coolDown);

        timerTxt.text = (int)coolDownCounterR + "";
        if ((int)coolDownCounterR == 0)
            timerTxt.text = "";

        canUseS = coolDownCounterR <= 0 && canvasGroup.blocksRaycasts && !AffectZoneManagerZS.Instance.isAffectZoneWorking && !AffectZoneManagerZS.Instance.isChecking;

        canvasGroup.interactable = canUseS;
    }

    private void ActiveLightingG()
    {
        AffectZoneManagerZS.Instance.ActiveZoneE(AffectZoneType.Lighting, this);
        SoundManagerZS.Click();
    }

    private void ActiveFrozenN()
    {
        AffectZoneManagerZS.Instance.ActiveZoneE(AffectZoneType.Frozen, this);
        SoundManagerZS.Click();
    }

    private void ActivePoisonN()
    {
        AffectZoneManagerZS.Instance.ActiveZoneE(AffectZoneType.Poison, this);
        SoundManagerZS.Click();
    }
    
    public void StartCountingDownN()
    {
        allowCountingG = true;
        coolDownCounterR = coolDown;
    }

    private void OnBtnClickK()
    {
        if (!canUseS)
            return;

        if (!allowWorkK)
            return;

        switch (affectTypeR)
        {
            case AffectZoneType.Lighting:
                ActiveLightingG();
                break;
            case AffectZoneType.Frozen:
                ActiveFrozenN();
                break;
            case AffectZoneType.Poison:
                ActivePoisonN();
                break;
        }

        allowWorkK = false;
        allowCountingG = false;
        
    }
}
