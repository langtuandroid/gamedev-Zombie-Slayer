using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class MenuManagerZS : MonoBehaviour, IListener
{
    public static MenuManagerZS Instance;
    [SerializeField] private GameObject startUI;
    [SerializeField] private  GameObject ui;
    [SerializeField] private  GameObject victotyUI;
    [SerializeField] private  GameObject failUI;
    [SerializeField] private  GameObject pauseUI;
    [SerializeField] private  GameObject loadingUI;
    [SerializeField] private  GameObject testOption;
    [SerializeField] private  GameObject missionInfor;
    [Header("Sound and Music")]
    [SerializeField] private  Image soundImage;
    [SerializeField] private  Image musicImage;
    [SerializeField] private  Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    [SerializeField] private  GameObject handDirection;
    [SerializeField] private  TextMeshProUGUI levelTxt;
    private UI_UI uiControlL;
    
    [Inject] private GameModeZS gameModeZs;

    private void Awake()
    {
        Instance = this;
        startUI.SetActive(false);
        victotyUI.SetActive(false);
        failUI.SetActive(false);
        pauseUI.SetActive(false);
        loadingUI.SetActive(false);
        missionInfor.SetActive(true);
        uiControlL = gameObject.GetComponentInChildren<UI_UI>(true);
        handDirection.SetActive(false);
        if (gameModeZs)
            testOption.SetActive(gameModeZs.showTestOption);
        levelTxt.text = "Level " + GlobalValueZS.LevelPlaying;
    }

    public void ShowMissionInfor(bool open)
    {
        SoundManagerZS.Click();
        missionInfor.SetActive(open);
    }

    public void BeginGame()
    {
        GameManagerZS.Instance.StartGameE();
        ShowMissionInfor(false);
    }

    private IEnumerator Start()
    {
       // soundImage.sprite = GlobalValueZS.IsSound ? soundImageOn : soundImageOff;
       // musicImage.sprite = GlobalValueZS.IsMusic ? musicImageOn : musicImageOff;
        if (!GlobalValueZS.IsSound)
            SoundManagerZS.SoundVolume = 0;
        if (!GlobalValueZS.IsMusic)
            SoundManagerZS.MusicVolume = 0;

        startUI.SetActive(true);

        yield return new WaitForSeconds(1);
        startUI.SetActive(false);
        ui.SetActive(true);
    }

    private float currentTimeScaleE;
    
    public void Pause()
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundPause);
        if (Time.timeScale != 0)
        {
            GameManagerZS.Instance.GamePause();
            currentTimeScaleE = Time.timeScale;
            Time.timeScale = 0;
            ui.SetActive(false);
            pauseUI.SetActive(true);
            SoundManagerZS.Instance.PauseMusicC(true);
        }
        else
        {
            GameManagerZS.Instance.UnPause();
            Time.timeScale = currentTimeScaleE;
            ui.SetActive(true);
            pauseUI.SetActive(false);
            SoundManagerZS.Instance.PauseMusicC(false);
        }
    }

    public void ShowHandDirection(float delay = 2)
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.moveOn);
        handDirection.SetActive(true);
        Invoke(nameof(HideHandDirection), delay);
    }

    private void HideHandDirection()
    {
        handDirection.SetActive(false);
    }

    public void IPlayY()
    {
       
    }

    public void ISuccessS()
    {
        StartCoroutine(VictoryCo());
    }

    private IEnumerator VictoryCo()
    {
        ui.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        victotyUI.SetActive(true);
    }


    public void IPauseE()
    {
      
    }

    public void IUnPauseE()
    {
        
    }

    public void IGameOverR()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        ui.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        failUI.SetActive(true);
    }

    public void IOnRespawnN()
    {
        
    }

    public void IOnStopMovingOnN()
    {
        
    }

    public void IOnStopMovingOffF()
    {
       
    }

    
    #region Music and Sound
    public void TurnSound()
    {
        GlobalValueZS.IsSound = !GlobalValueZS.IsSound;
        soundImage.sprite = GlobalValueZS.IsSound ? soundImageOn : soundImageOff;

        SoundManagerZS.SoundVolume = GlobalValueZS.IsSound ? 1 : 0;
    }

    public void TurnMusic()
    {
        GlobalValueZS.IsMusic = !GlobalValueZS.IsMusic;
        musicImage.sprite = GlobalValueZS.IsMusic ? musicImageOn : musicImageOff;

        SoundManagerZS.MusicVolume = GlobalValueZS.IsMusic ? SoundManagerZS.Instance.musicsGameVolume : 0;
    }
    #endregion

    #region Load Scene
    public void LoadHomeMenuScene()
    {
        SoundManagerZS.Click();
        StartCoroutine(LoadAsynchronously("Menu"));
    }

    public void RestarLevel()
    {
        SoundManagerZS.Click();
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void LoadNextLevel()
    {
        SoundManagerZS.Click();

        if (GlobalValueZS.LevelPlaying >= 100)
        {
            LoadHomeMenuScene();
            return;
        }
        
        GlobalValueZS.LevelPlaying++;

        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    [Header("Load scene")]
    public Slider slider;
    public Text progressText;
    private IEnumerator LoadAsynchronously(string name)
    {
        loadingUI.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            //			Debug.LogError (progress);
            yield return null;
        }
    }
    #endregion

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
