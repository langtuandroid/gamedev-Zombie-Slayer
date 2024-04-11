using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public enum GUNTYPE { typeA, typeB}

    public class GunTypeIDZS : MonoBehaviour
    {
        [SerializeField] private bool unlockDefault = false;
        public GUNTYPE gunType;
        public string gunID = "gun ID";
        public Sprite icon;
        [FormerlySerializedAs("unlockPrice")] public int unlockPriceE = 900;
        [FormerlySerializedAs("animatorOverride")]
        [Header("ANIMATION")]
        public AnimatorOverrideController animatorOverrideE;
        [Header("WEAPONS")]
      
        [SerializeField] public int maxBullet = 99;
        [FormerlySerializedAs("shootingMethob")] public ShootingMethob shootingMethod;
        [FormerlySerializedAs("minPercentAffect")]
        [Range(0, 100)]
        [SerializeField] public int minPercentAffecT = 90;
        [FormerlySerializedAs("rate")] public float rateE = 0.2f;
        [FormerlySerializedAs("reloadTime")]  public float reloadTimeE = 2;
        [FormerlySerializedAs("accuracy")]
        [Range(0.5f, 1f)]
        [SerializeField] public float accuracyY = 0.9f;
        [FormerlySerializedAs("shellFX")] public GameObject shellFXx;
        [FormerlySerializedAs("shellPoint")] public Transform shellPointT;

        [FormerlySerializedAs("soundFire")] public AudioClip soundFireE;
        [FormerlySerializedAs("soundFireVolume")]
        [Range(0, 1)]
        [SerializeField] public float soundFireVolumeE = 0.5f;
        [FormerlySerializedAs("shellSound")] public AudioClip shellSoundD;
        [FormerlySerializedAs("shellSoundVolume")]
        [Range(0, 1)]
        [SerializeField] public float shellSoundVolumeE = 0.5f;
        [FormerlySerializedAs("reloadSound")] public AudioClip reloadSoundD;
        [FormerlySerializedAs("reloadSoundVolume")]
        [Range(0, 1)]
        [SerializeField] public float reloadSoundVolumeE = 0.5f;
        [FormerlySerializedAs("reloadPerShoot")] public bool reloadPerShootT = false;
        [FormerlySerializedAs("dualShot")] public bool dualShotT = false;
        [FormerlySerializedAs("fireSecondGunDelay")] public float fireSecondGunDelayY = 0.1f;
        [FormerlySerializedAs("isSpreadBullet")] public bool isSpreadBulletT = false;
        [FormerlySerializedAs("maxBulletPerShoot")] public int maxBulletPerShootT = 1;

        [FormerlySerializedAs("muzzleTracerFX")] public GameObject muzzleTracerFXx;
        [FormerlySerializedAs("muzzleFX")] public GameObject muzzleFXx;

        public void ResetBulletT()
        {
            Bullet = maxBullet;
        }

        public int Bullet
        {
            get { return PlayerPrefs.GetInt("gunID" + gunID, maxBullet); }
            set { PlayerPrefs.SetInt("gunID" + gunID, Mathf.Min(value, maxBullet)); }
        }

        public bool IsUnlocked
        {
            get { return (PlayerPrefs.GetInt("isUnlocked" + gunID, 0) == 1) || unlockDefault; }
            set { PlayerPrefs.SetInt("isUnlocked" + gunID, value ? 1 : 0); }
        }

        [FormerlySerializedAs("UpgradeSteps")]
        [Header("UPGRADE")]
        [Space]
        public UpgradeStep[] upgradeSteps;

        public int CurrentUpgrade
        {
            get
            {
                int current = PlayerPrefs.GetInt(gunID + "upgrade" + "Current", 0);
                if (current >= upgradeSteps.Length)
                    current = -1;   //-1 mean overload
                return current;
            }
            set
            {
                PlayerPrefs.SetInt(gunID + "upgrade" + "Current", value);
            }
        }

        public void UpgradeCharacterR()
        {
            CurrentUpgrade++;
            UpgradeRangeDamage = upgradeSteps[CurrentUpgrade].damage;
        }

        public int UpgradeRangeDamage
        {
            get => PlayerPrefs.GetInt(gunID + "UpgradeRangeDamage", upgradeSteps[0].damage);
            private set { PlayerPrefs.SetInt(gunID + "UpgradeRangeDamage", value); }
        }
    }

    [System.Serializable]
    public class UpgradeStep
    {
        public int price;
        public int damage;
    }
}