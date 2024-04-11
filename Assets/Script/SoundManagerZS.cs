using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

/*
 * This is SoundManager
 * In other script, you just need to call SoundManager.PlaySfx(AudioClip) to play the sound
*/
public class SoundManagerZS : MonoBehaviour {
	public static SoundManagerZS Instance;
    public AudioClip beginSoundInMainMenu;
    [Tooltip("Play music clip when start")]
	public AudioClip musicsGame;
	[Range(0,1)]
	public float musicsGameVolume = 0.5f;

	[Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
	public AudioClip soundClick; 
    [Header("Game State")]
    public AudioClip soundFail;
    public AudioClip soundPause;
    public AudioClip soundShowStagePanel;

	public AudioClip swapGun;
	public AudioClip chooseGun;
	public AudioClip moveOn;
	[Header("Shop")]
    public AudioClip soundPurchased;
    public AudioClip soundUpgrade;
    public AudioClip soundNotEnoughCoin;
	public AudioClip soundUnlockGun;

    [Header("Victory")]
    public AudioClip soundVictoryPanel;

    private AudioSource musicAudioO;
	private AudioSource soundFxX;

    //[Header("BOOST ITEM")]
	[HideInInspector] public AudioClip bTsoundOpen;
	[HideInInspector] public AudioClip bTsoundHide;
	[HideInInspector] public AudioClip bTsoundUseBoost;

    //public AudioClip switchPlayerSound;

    //public AudioClip soundCheckpoint;
    public void PauseMusicC(bool isPause){
		if (isPause)
			Instance.musicAudioO.mute = true;
//			Instance.musicAudio.Pause ();
		else
			Instance.musicAudioO.mute = false;
//			Instance.musicAudio.UnPause ();
	}
	//GET and SET
	public static float MusicVolume{
		
		set => Instance.musicAudioO.volume = value;
		get => Instance.musicAudioO.volume;
	}
	public static float SoundVolume{
		set => Instance.soundFxX.volume = value;
		get => Instance.soundFxX.volume;
	}
	// Use this for initialization
	private void Awake(){
		Instance = this;
		musicAudioO = gameObject.AddComponent<AudioSource> ();
		musicAudioO.loop = true;
		musicAudioO.volume = 0.5f;
		soundFxX = gameObject.AddComponent<AudioSource> ();
	}
	private void Start () {
//		//Check auido and sound
//		if (!GlobalValue.isMusic)
//			musicAudio.volume = 0;
//		if (!GlobalValue.isSound)
//			soundFx.volume = 0;
//		PlayMusic(musicsGame,musicsGameVolume);
		//		//Check auido and sound

		PlayMusicC (musicsGame, musicsGameVolume);
	}

	public static void Click(){
		PlaySfx (Instance.soundClick);
	}

	public  void ClickBut(){
		PlaySfx (soundClick);
	}

	public static void PlaySfx(AudioClip clip){
		if (Instance != null) {
			Instance.PlaySoundD (clip, Instance.soundFxX);
		}


	}

    public static void PlaySfx(AudioClip clip, float volume)
    {
        if (Instance != null)
            Instance.PlaySoundD(clip, Instance.soundFxX, volume);
    }

    public static void PlaySfx(AudioClip[] clips)
    {
        if (Instance != null && clips.Length > 0)
            Instance.PlaySoundD(clips[Random.Range(0, clips.Length)], Instance.soundFxX);
    }

    public static void PlaySfx(AudioClip[] clips, float volume)
    {
        if (Instance != null && clips.Length > 0)
            Instance.PlaySoundD(clips[Random.Range(0, clips.Length)], Instance.soundFxX, volume);
    }

    public static void PlayMusicC(AudioClip clip){
		Instance.PlaySoundD (clip, Instance.musicAudioO);
	}

	public static void PlayMusicC(AudioClip clip, float volume){
		Instance.PlaySoundD (clip, Instance.musicAudioO, volume);
	}

	private void PlaySoundD(AudioClip clip,AudioSource audioOut){
		if (clip == null) {
			//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (Instance == null)
			return;

		if (audioOut == musicAudioO) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume);
	}

	private void PlaySoundD(AudioClip clip,AudioSource audioOut, float volume){
		if (clip == null) {
			//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

        if (audioOut == musicAudioO)
        {
            //if (!GlobalValue.isMusic) return;
            audioOut.volume = GlobalValueZS.IsMusic? volume:0;
            audioOut.clip = clip;
            audioOut.Play();
        }
        else
        {
            if (!GlobalValueZS.IsSound) return;
            audioOut.PlayOneShot(clip, SoundVolume * volume);
        }
	}
}
