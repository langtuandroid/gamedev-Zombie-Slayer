using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuManager : MonoBehaviour, IListener
{
    public static MenuManager Instance;
    public GameObject StartUI;
    public GameObject UI;
    public GameObject VictotyUI;
    public GameObject FailUI;
    public GameObject PauseUI;
    public GameObject LoadingUI;
    public GameObject TestOption;
    public GameObject MissionInfor;
    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    public GameObject handDirection;
    public Text levelTxt;
    private UI_UI uiControlL;
    
    [Inject] private GameModeZS gameModeZs;

    private void Awake()
    {
        Instance = this;
        StartUI.SetActive(false);
        VictotyUI.SetActive(false);
        FailUI.SetActive(false);
        PauseUI.SetActive(false);
        LoadingUI.SetActive(false);
        MissionInfor.SetActive(true);
        uiControlL = gameObject.GetComponentInChildren<UI_UI>(true);
        handDirection.SetActive(false);
        if (gameModeZs)
            TestOption.SetActive(gameModeZs.showTestOption);
        levelTxt.text = "Level " + GlobalValueZS.LevelPlaying;
    }

    public void ShowMissionInfor(bool open)
    {
        SoundManagerZS.Click();
        MissionInfor.SetActive(open);
    }

    public void BeginGame()
    {
        GameManagerZS.Instance.StartGameE();
        ShowMissionInfor(false);
    }

    IEnumerator Start()
    {
        soundImage.sprite = GlobalValueZS.IsSound ? soundImageOn : soundImageOff;
        musicImage.sprite = GlobalValueZS.IsMusic ? musicImageOn : musicImageOff;
        if (!GlobalValueZS.IsSound)
            SoundManagerZS.SoundVolume = 0;
        if (!GlobalValueZS.IsMusic)
            SoundManagerZS.MusicVolume = 0;

        StartUI.SetActive(true);

        yield return new WaitForSeconds(1);
        StartUI.SetActive(false);
        UI.SetActive(true);
    }

    float currentTimeScale;
    public void Pause()
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.soundPause);
        if (Time.timeScale != 0)
        {
            GameManagerZS.Instance.GamePause();
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0;
            UI.SetActive(false);
            PauseUI.SetActive(true);
            SoundManagerZS.Instance.PauseMusicC(true);
        }
        else
        {
            GameManagerZS.Instance.UnPause();
            Time.timeScale = currentTimeScale;
            UI.SetActive(true);
            PauseUI.SetActive(false);
            SoundManagerZS.Instance.PauseMusicC(false);
        }
    }

    public void ShowHandDirection(float delay = 2)
    {
        SoundManagerZS.PlaySfx(SoundManagerZS.Instance.moveOn);
        handDirection.SetActive(true);
        Invoke("HideHandDirection", delay);
    }

   void HideHandDirection()
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

    IEnumerator VictoryCo()
    {
        UI.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        VictotyUI.SetActive(true);
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

    IEnumerator GameOverCo()
    {
        UI.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        FailUI.SetActive(true);
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
        GlobalValueZS.LevelPlaying++;
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    [Header("Load scene")]
    public Slider slider;
    public Text progressText;
    IEnumerator LoadAsynchronously(string name)
    {
        LoadingUI.SetActive(true);

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
