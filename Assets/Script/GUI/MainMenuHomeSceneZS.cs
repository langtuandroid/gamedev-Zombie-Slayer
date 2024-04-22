using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class MainMenuHomeSceneZS : MonoBehaviour {
    
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject testOption;
    [SerializeField] private GameObject chooseWeapon;
    [SerializeField] private GameObject exitUI;

	[SerializeField] private string facebookLink;
    [SerializeField] private string twitterLink = "https://twitter.com/";

    [SerializeField] private TextMeshProUGUI[] coinTxt;

    [Header("Sound and Music")]
    [SerializeField] private Image soundImage;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI progressText;
    
    [Inject] private GameModeZS gameModeZs;
    
    private void Awake(){
        startUI.SetActive(true);
        if (loading != null)
			loading.SetActive (false);
		if (mapUI != null)
            mapUI.SetActive (false);
        if (settings)
            settings.SetActive(false);
        shop.SetActive(false);
        chooseWeapon.SetActive(false);
        exitUI.SetActive(false);

        if (gameModeZs)
            testOption.SetActive(gameModeZs.showTestOption);
    }

    public void OpenShop(bool open)
    {
        SoundManagerZS.Click();
        shop.SetActive(open);
    }

    public void OpenExitGame(bool show)
    {
        SoundManagerZS.Click();
        exitUI.SetActive(show);
    }

    public void QuitGame()
    {
        SoundManagerZS.Click();
        Application.Quit();
    }

    public void ShowChooseWeapon(bool open)
    {
        SoundManagerZS.Click();
        chooseWeapon.SetActive(open);
    }

    public void OpenMenuScene()
    {
        SoundManagerZS.Click();
        startUI.SetActive(false);
    }

    public void LoadScene(){
		if (loading != null)
			loading.SetActive (true);
        
        StartCoroutine(LoadAsynchronously("Playing"));
    }

    public void LoadScene(string sceneNamage)
    {
        if (loading != null)
            loading.SetActive(true);

        StartCoroutine(LoadAsynchronously(sceneNamage));
    }
    
	private IEnumerator Start () {
		CheckSoundMusic();
        if (GlobalValueZS.IsFirstOpenMainMenu)
        {
            GlobalValueZS.IsFirstOpenMainMenu = false;
            SoundManagerZS.Instance.PauseMusicC(true);
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.beginSoundInMainMenu);
            yield return new WaitForSeconds(SoundManagerZS.Instance.beginSoundInMainMenu.length);
            SoundManagerZS.Instance.PauseMusicC(false);
            SoundManagerZS.PlayMusicC(SoundManagerZS.Instance.musicsGame);
        }
    }

    private void Update() {
        CheckSoundMusic();

        foreach (var ct in coinTxt)
        {
            ct.text = GlobalValueZS.SavedCoins + "";
        }
	}

	public void OpenMap(bool open){
        SoundManagerZS.Click();
        StartCoroutine(OpenMapCo(open));
	}

    private IEnumerator OpenMapCo(bool open)
    {
        yield return null;
        BlackScreenUIZS.Instance.Show(0.2f);
        mapUI.SetActive(open);
        BlackScreenUIZS.Instance.Hide(0.2f);
    }

	public void Facebook(){
        SoundManagerZS.Click();
		Application.OpenURL (facebookLink);
	}

    public void Twitter()
    {
        SoundManagerZS.Click();
        Application.OpenURL(twitterLink);
    }

    public void ExitGame()
    {
        SoundManagerZS.Click();
        Application.Quit();
    }

    public void Setting(bool open)
    {
        SoundManagerZS.Click();
        settings.SetActive(open);
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
        //musicImage.sprite = GlobalValueZS.IsMusic ? musicImageOn : musicImageOff;

        SoundManagerZS.MusicVolume = GlobalValueZS.IsMusic ? SoundManagerZS.Instance.musicsGameVolume : 0;
    }
    #endregion

    private void CheckSoundMusic(){
        soundImage.sprite = GlobalValueZS.IsSound ? soundImageOn : soundImageOff;
        musicImage.sprite = GlobalValueZS.IsMusic ? musicImageOn : musicImageOff;
        SoundManagerZS.SoundVolume = GlobalValueZS.IsSound ? 1 : 0;
        SoundManagerZS.MusicVolume = GlobalValueZS.IsMusic ? SoundManagerZS.Instance.musicsGameVolume : 0;
    }

    public void Tutorial(){
		SoundManagerZS.Click ();
		SceneManager.LoadScene ("Tutorial");
	}

   
    private IEnumerator LoadAsynchronously(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)progress * 100f + "%";
            yield return null;
        }
    }

    public void ResetData()
    {
        if (gameModeZs)
            gameModeZs.ResetData();
    }

    public void SetMaxCoin()
    {
        GlobalValueZS.SavedCoins = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void UnlockAll()
    {
        GlobalValueZS.LevelPass = 99999;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
