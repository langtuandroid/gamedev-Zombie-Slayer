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
        [SerializeField] private Sprite icon;
        [FormerlySerializedAs("unlockPrice")] [SerializeField] private int unlockPriceE = 900;
        [FormerlySerializedAs("animatorOverride")]
        [Header("ANIMATION")]
        [SerializeField] private AnimatorOverrideController animatorOverrideE;
        [Header("WEAPONS")]
        //public UpgradedCharacterParameter upgradedCharacterID;
        [SerializeField] private int maxBullet = 99;
        [FormerlySerializedAs("shootingMethob")] [SerializeField] private ShootingMethob shootingMethod;
        [FormerlySerializedAs("minPercentAffect")]
        [Range(0, 100)]
        [SerializeField] private int minPercentAffecT = 90;
        [FormerlySerializedAs("rate")] [SerializeField] private float rateE = 0.2f;
        [FormerlySerializedAs("reloadTime")] [SerializeField] private float reloadTimeE = 2;
        [FormerlySerializedAs("accuracy")]
        [Range(0.5f, 1f)]
        [SerializeField] private float accuracyY = 0.9f;
        [FormerlySerializedAs("shellFX")] [SerializeField] private GameObject shellFXx;
        [FormerlySerializedAs("shellPoint")] [SerializeField] private Transform shellPointT;

        [FormerlySerializedAs("soundFire")] [SerializeField] private AudioClip soundFireE;
        [FormerlySerializedAs("soundFireVolume")]
        [Range(0, 1)]
        [SerializeField] private float soundFireVolumeE = 0.5f;
        [FormerlySerializedAs("shellSound")] [SerializeField] private AudioClip shellSoundD;
        [FormerlySerializedAs("shellSoundVolume")]
        [Range(0, 1)]
        [SerializeField] private float shellSoundVolumeE = 0.5f;
        [FormerlySerializedAs("reloadSound")] [SerializeField] private AudioClip reloadSoundD;
        [FormerlySerializedAs("reloadSoundVolume")]
        [Range(0, 1)]
        [SerializeField] private float reloadSoundVolumeE = 0.5f;
        [FormerlySerializedAs("reloadPerShoot")] [SerializeField] private bool reloadPerShootT = false;
        [FormerlySerializedAs("dualShot")] [SerializeField] private bool dualShotT = false;
        [FormerlySerializedAs("fireSecondGunDelay")] [SerializeField] private float fireSecondGunDelayY = 0.1f;
        [FormerlySerializedAs("isSpreadBullet")] [SerializeField] private bool isSpreadBulletT = false;
        [FormerlySerializedAs("maxBulletPerShoot")] [SerializeField] private int maxBulletPerShootT = 1;

        [FormerlySerializedAs("muzzleTracerFX")] [SerializeField] private GameObject muzzleTracerFXx;
        [FormerlySerializedAs("muzzleFX")] [SerializeField] private GameObject muzzleFXx;

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