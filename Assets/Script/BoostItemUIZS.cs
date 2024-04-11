using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BoostItemUIZS : MonoBehaviour
{
    [ReadOnly] public WEAPON_EFFECT currentEffectT = WEAPON_EFFECT.NONE;
    [ReadOnly] public NumberArrow currentNumberOfArrowsS = NumberArrow.Single;

    [FormerlySerializedAs("DA_remainTxt")]
    [Header("Double Arrow")]
    [SerializeField] private Text daRemainTxt;
    [FormerlySerializedAs("DA_Button")] [SerializeField] private Button daButton;
    [FormerlySerializedAs("DA_Icon")] [SerializeField] private GameObject daIcon;
    [FormerlySerializedAs("DA_timerTxt")] [SerializeField] private Text daTimerTxt;
    [FormerlySerializedAs("DA_Time")] [SerializeField] private int daTime = 25;
    [FormerlySerializedAs("DA_TimeCounter")] [ReadOnly] public float daTimeCounter = 0;
 
    [FormerlySerializedAs("PA_remainTxt")]
    [Header("Poison Arrow")]
    [SerializeField] private Text paRemainTxt;
    [FormerlySerializedAs("PA_Button")] [SerializeField] private Button paButton;
    [FormerlySerializedAs("PA_Icon")] [SerializeField] private GameObject paIcon;
    [FormerlySerializedAs("PA_timerTxt")] [SerializeField] private Text paTimerTxt;
    [FormerlySerializedAs("PA_Time")] [SerializeField] private int paTime = 30;
    [FormerlySerializedAs("PA_TimeCounter")] [ReadOnly] public float paTimeCounter = 0;
    [FormerlySerializedAs("FA_remainTxt")]
    [Header("Freeze Arrow")]
    [SerializeField] private Text faRemainTxt;
    [FormerlySerializedAs("FA_Button")] [SerializeField] private Button faButton;
    [FormerlySerializedAs("FA_Icon")] [SerializeField] private GameObject faIcon;
    [FormerlySerializedAs("FA_timerTxt")] [SerializeField] private Text faTimerTxt;
    [FormerlySerializedAs("FA_Time")] [SerializeField] private int faTime = 30;
    [ReadOnly] public float faTimeCounter = 0;

    [Header("Boost Item")]
    [SerializeField] private Animator boostItemAnim;
    [SerializeField] private Animator boostButtonAnim;
    [SerializeField] private float boostItemAutoHide = 3;

    [Space]
    public GameObject activeIcons;
    
    private void Start()
    {
        daRemainTxt.text = "x" + GlobalValueZS.ItemDoubleArrow;
        //TA_remainTxt.text = "x" + GlobalValue.ItemTripleArrow;
        paRemainTxt.text = "x" + GlobalValueZS.ItemPoison;
        faRemainTxt.text = "x" + GlobalValueZS.ItemFreeze;

        daButton.interactable = GlobalValueZS.ItemDoubleArrow > 0;
        //TA_Button.interactable = GlobalValue.ItemTripleArrow > 0;
        paButton.interactable = GlobalValueZS.ItemPoison > 0;
        faButton.interactable = GlobalValueZS.ItemFreeze > 0;

        daIcon.SetActive(false);
        //TA_Icon.SetActive(false);
        paIcon.SetActive(false);
        faIcon.SetActive(false);
    }

    private void Update()
    {
        activeIcons.SetActive(daIcon.activeSelf /*|| TA_Icon.activeSelf */|| paIcon.activeSelf || faIcon.activeSelf);
    }

    #region Double Arrow
    public void ActiveDoubleArror()
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.bTsoundUseBoost);
        GlobalValueZS.ItemDoubleArrow--;
        daRemainTxt.text = "x" + GlobalValueZS.ItemDoubleArrow;
        daButton.interactable = false;     //only active per game level

        currentNumberOfArrowsS = NumberArrow.Double;
        //DA_Icon.SetActive(true);
        RunTimerAutoHideBoostPanel();
        doubleArrowTimerCoDo = DoubleArrowTimerCo();
        StartCoroutine(doubleArrowTimerCoDo);
    }

    private IEnumerator doubleArrowTimerCoDo;
    private IEnumerator DoubleArrowTimerCo()
    {
        daIcon.SetActive(true);

        daTimeCounter = (float)daTime;
        while (daTimeCounter > 0)
        {
            daTimeCounter -= Time.deltaTime;
            daTimerTxt.text = (int)daTimeCounter + "";
            yield return null;
        }

        daIcon.SetActive(false);
        daButton.interactable = true && GlobalValueZS.ItemDoubleArrow > 0;
        currentNumberOfArrowsS = NumberArrow.Single;
    }
    #endregion

    #region Poison Arrow
    public void ActivePoisonArrow() {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.bTsoundUseBoost);
        GlobalValueZS.ItemPoison--;
        paRemainTxt.text = "x" + GlobalValueZS.ItemPoison;
        paButton.interactable = false; 
        faButton.interactable = false;
        currentEffectT = WEAPON_EFFECT.POISON;

        RunTimerAutoHideBoostPanel();
        StartCoroutine(PoisonArrowTimerCoO());
    }

    private IEnumerator PoisonArrowTimerCoO()
    {
        paIcon.SetActive(true);
       
        paTimeCounter = (float)paTime;
        while (paTimeCounter > 0)
        {
            paTimeCounter -= Time.deltaTime;
            paTimerTxt.text = (int)paTimeCounter + "";
           yield return null;
        }

        paIcon.SetActive(false);
        paButton.interactable = true && GlobalValueZS.ItemPoison > 0;
        faButton.interactable = true && GlobalValueZS.ItemFreeze > 0;
        currentEffectT = WEAPON_EFFECT.NONE;
    }
    #endregion

    #region Freeze Arrow
    public void ActiveFreezeArrow() {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.bTsoundUseBoost);
        GlobalValueZS.ItemFreeze--;
        faRemainTxt.text = "x" + GlobalValueZS.ItemFreeze;
        faButton.interactable = false;
        paButton.interactable = false;
        currentEffectT = WEAPON_EFFECT.FREEZE;

        RunTimerAutoHideBoostPanel();

        StartCoroutine(FreezeArrowTimerCoO());
    }

    private IEnumerator FreezeArrowTimerCoO()
    {
        faIcon.SetActive(true);
        faTimeCounter = (float)faTime;
        while(faTimeCounter > 0)
        {
            faTimeCounter -= Time.deltaTime;
            faTimerTxt.text = (int)faTimeCounter + "";
            yield return null;
        }

        faIcon.SetActive(false);
        paButton.interactable = true && GlobalValueZS.ItemPoison > 0;
        faButton.interactable = true && GlobalValueZS.ItemFreeze > 0;
        currentEffectT = WEAPON_EFFECT.NONE;
    }
    #endregion

    #region BOOST PANEL
    private IEnumerator boostItemHideCoDo;
    
    public void BoostItem()
    {
        if (boostItemAnim.GetBool("show"))
        {
            HideBoostPanelL();
        }
        else
        {
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.bTsoundOpen);
            boostItemAnim.SetBool("show", true);
            boostButtonAnim.SetBool("on", true);
            RunTimerAutoHideBoostPanel();
        }
    }

    private void RunTimerAutoHideBoostPanel()
    {
        if (boostItemHideCoDo != null)
            StopCoroutine(boostItemHideCoDo);

        boostItemHideCoDo = BoostItemHideCoo(boostItemAutoHide);
        StartCoroutine(boostItemHideCoDo);
    }

    private IEnumerator BoostItemHideCoo(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideBoostPanelL();
    }

    private void HideBoostPanelL()
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.bTsoundHide);
        boostItemAnim.SetBool("show", false);
        boostButtonAnim.SetBool("on", false);
    }

    #endregion
}
