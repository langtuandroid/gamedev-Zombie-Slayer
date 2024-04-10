using UnityEngine;
using System.Collections;
using Script;

public class GlobalValueZS : MonoBehaviour {
    public static bool IsFirstOpenMainMenu = true;
	public static int WorldPlaying = 1;
	public static int LevelPlaying = 1;
    //public static int finishGameAtLevel = 50;

    public static string WorldReached = "WorldReached";
	public static bool IsSound = true;
	public static bool IsMusic = true;

    public static bool IsNewGame
    {
        get { return PlayerPrefs.GetInt("isNewGame", 0) == 0; }
        set { PlayerPrefs.SetInt("isNewGame", value ? 0 : 1); }
    }

    public static bool IsEarnCoin
    {
        get { return PlayerPrefs.GetInt("isEarnCoin", 0) == 1; }
        set { PlayerPrefs.SetInt("isEarnCoin", value ? 1 : 0); }
    }

    public static int LastDayShowNativeAd1{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd1", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd1", value); }
	}

	public static int LastDayShowNativeAd2{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd2", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd2", value); }
	}

    public static int GetBullet(int ID, int defaultBullet )
    {
        return PlayerPrefs.GetInt("GetBullet" + ID, defaultBullet);
    }

    public static void SetBullet(int ID, int value)
    {
        PlayerPrefs.SetInt("GetBullet" + ID, value);
    }

    public static int LastDayShowNativeAd3{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd3", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd3", value); }
	}

	public static int SavedCoins
    {
        get { return PlayerPrefs.GetInt("Coins", 200); }
        set
        {
            IsEarnCoin = true;
            PlayerPrefs.SetInt("Coins", value);
        }
    }
    
    public static int LevelPass { 
		get { return PlayerPrefs.GetInt ("LevelReached", 0); } 
		set { PlayerPrefs.SetInt ("LevelReached", value); } 
	}

	public static void LevelStar(int level, int stars){
		PlayerPrefs.SetInt ("LevelStars" + level, stars);
	}

	public static int LevelStar(int level){
		return PlayerPrefs.GetInt ("LevelStars" + level, 0); 
	}

	public static bool RemoveAds { 
		get { return PlayerPrefs.GetInt ("RemoveAds", 0) == 1 ? true : false; } 
		set { PlayerPrefs.SetInt ("RemoveAds", value ? 1 : 0); } 
	}

    public static int ItemDoubleArrow
    {
        get { return PlayerPrefs.GetInt("ItemDoubleArrow", 3); }
        set { PlayerPrefs.SetInt("ItemDoubleArrow", value); }
    }

    public static int ItemTripleArrow
    {
        get { return PlayerPrefs.GetInt("ItemTripleArrow", 1); }
        set => PlayerPrefs.SetInt("ItemTripleArrow", value);
    }

    public static int ItemPoison
    {
        get { return PlayerPrefs.GetInt("ItemPoison", 3); }
        set => PlayerPrefs.SetInt("ItemPoison", value);
    }

    public static int ItemFreeze
    {
        get { return PlayerPrefs.GetInt("ItemFreeze", 3); }
        set { PlayerPrefs.SetInt("ItemFreeze", value); }
    }

    public static bool isPicked(GunTypeIDZS gunIdzs)
    {
       return PlayerPrefs.GetString("GUNTYPE" + gunIdzs.gunType, "") == gunIdzs.gunID;
    }

    public static void pickGun(GunTypeIDZS gunIdzs)
    {
        PlayerPrefs.SetString("GUNTYPE" + gunIdzs.gunType, gunIdzs.gunID);
    }
}