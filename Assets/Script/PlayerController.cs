using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

namespace Script
{
    public enum GunHandlerState { AVAILABLE, SWAPPING, RELOADING, EMPTY }
    public enum ShootingMethob { SingleShoot, AutoShoot}
    public enum WeaponState { MELEE, GUN}
    [RequireComponent(typeof(PlayerSpineHelper))]
    public class PlayerController : MonoBehaviour, ICanTakeDamage
    {
        [Header("SET UP")]

        public Animator anim;
        public float moveSpeed = 10;
        public float limitAbovePos = -0.8f;
        public float limitBelowPos = -3.65f;
        [ReadOnly] [SerializeField] private float limitLeft;
        [ReadOnly] [SerializeField] private float limitRight;
        public AudioClip soundHurt, soundDie;

        [Header("BLOCK LAYER")]
        public LayerMask blockWayLayerMask;

        [Header("HEALTH")]
        [Range(0, 5000)]
        [SerializeField] public int health = 100;
        [SerializeField] public Vector2 healthBarOffset = new Vector2(0, 1.5f);

        [SerializeField] private float currentHealthH;

        [Header("GRENADE")]
        [SerializeField] private GameObject grenade;
        [SerializeField] private Transform throwPoint;

        [Header("WEAPONS")]
        [ReadOnly]  [SerializeField] private WeaponState weaponState;
        [FormerlySerializedAs("GunState")] [ReadOnly]  [SerializeField] private GunHandlerState gunState;
        [FormerlySerializedAs("gunTypeID")] [ReadOnly] public GunTypeIDZS gunTypeIdzs;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float lastTimeShootingG = -999;
        [SerializeField] private AudioClip throwGrenadeSound;
        [SerializeField] private bool allowShootingG = true;

        private HealthBarEnemyNewZS healthBarR;
        private PlayerSpineHelper playerSpineHelperR;
    
        public bool IsFacingRight => transform.rotation.eulerAngles.y == 0;

        private void Start()
        {
            if (anim == null)
                anim = GetComponent<Animator>();

            GameManagerZS.Instance.player = this;
            gunTypeIdzs = GunManagerZS.Instance.GetGunID();
            SetGun(gunTypeIdzs);
            GunManagerZS.Instance.ResetGunBullet();

            playerSpineHelperR = GetComponent<PlayerSpineHelper>();
            playerMeleeWeaponZs = GetComponent<PlayerMeleeWeaponZS>();

            currentHealthH = health;
            var healthBarObj = (HealthBarEnemyNewZS)Resources.Load("HealthBar", typeof(HealthBarEnemyNewZS));
            healthBarR = (HealthBarEnemyNewZS)Instantiate(healthBarObj, healthBarOffset, Quaternion.identity);

            healthBarR.Init(transform, (Vector3)healthBarOffset);
        }

        private void OnEnable()
        {
            if (GameManagerZS.Instance)
                GameManagerZS.Instance.player = this;
        }
        public Vector2 finalSpeed;

        private void GetLimitHorizontal()
        {
            limitLeft = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 0.5f;
            limitRight = Camera.main.ViewportToWorldPoint(Vector3.right).x - 0.5f;
        }

        private void Update()
        {
            if (GameManagerZS.Instance.state != GameManagerZS.GameState.Playing)
                return;

            GetInput();

            finalSpeed = inputT * moveSpeed * Time.deltaTime;

            GetLimitHorizontal();
            if (finalSpeed.x > 0 && transform.position.x >= limitRight)
                finalSpeed.x = 0;
            else if (finalSpeed.x < 0 && transform.position.x <= limitLeft)
                finalSpeed.x = 0;

            if ((finalSpeed.x > 0 && !IsFacingRight) || (finalSpeed.x < 0 && IsFacingRight))
                FlipT();

       

            if (!IsWayBlocked())
            {
                transform.Translate(finalSpeed, Space.World);

                if(transform.position.y > limitAbovePos)
                {
                    transform.position = new Vector3(transform.position.x, limitAbovePos, transform.position.z);
                }else if (transform.position.y < limitBelowPos)
                {
                    transform.position = new Vector3(transform.position.x, limitBelowPos, transform.position.z);
                }
            }

            healthBarR.transform.localScale = new Vector2(transform.localScale.x > 0 ? Mathf.Abs(healthBarR.transform.localScale.x) : -Mathf.Abs(healthBarR.transform.localScale.x), healthBarR.transform.localScale.y);
            AnimSetFloatT("speed", inputT.magnitude);
        }

        private bool IsWayBlocked()
        {
            return Physics2D.Raycast(transform.position, inputT, 0.2f, blockWayLayerMask);
        }

        private Vector2 inputT;
        private void GetInput()
        {
            inputT = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") + Input.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical") + Input.GetAxis("Vertical"));

            if (gunTypeIdzs.shootingMethod == ShootingMethob.SingleShoot)
            {
                if (CrossPlatformInputManager.GetButtonDown("Shoot"))
                {
                    Shoot();
                }
            }

            else if (gunTypeIdzs.shootingMethod == ShootingMethob.AutoShoot)
            {
                if (CrossPlatformInputManager.GetButton("Shoot"))
                {
                    Shoot();
                }
            }


            if (CrossPlatformInputManager.GetButtonDown("Melee"))
            {
                MeleeAttack();
            }
        }

        private void FlipT()
        {
            transform.rotation = Quaternion.Euler(0, IsFacingRight ? 180 : 0, 0);
        }

        public void AnimSetTrigger(string name)
        {
            anim.SetTrigger(name);
        }

        public void AnimSetSpeed(float value)
        {
            if (anim)
                anim.speed = value;
        }

        public void AnimSetFloatT(string name, float value)
        {
            anim.SetFloat(name, value);
        }

        public void AnimSetBoolL(string name, bool value)
        {
            anim.SetBool(name, value);
        }

        public void SetStateT(GunHandlerState state)
        {
            gunState = state;
        }

        private void SetAvailabeAfterSwapT()
        {

            SetStateT(GunHandlerState.AVAILABLE);
            Debug.Log("SetAvailabeAfterSwap");
            CheckBulletRemain();
        }

        public void Shoot()
        {
            if (weaponState == WeaponState.MELEE)
            {
                SetGun(GunManagerZS.Instance.GetGunID());
                return;
            }

            if (!allowShootingG || gunTypeIdzs.Bullet <= 0)
                return;

            if (Time.time < (lastTimeShootingG + gunTypeIdzs.rateE))
                return;

            if (gunState != GunHandlerState.AVAILABLE)
                return;

            lastTimeShootingG = Time.time;
            gunTypeIdzs.Bullet--;
            AnimSetTrigger("shoot");
            for (int i = 0; i < gunTypeIdzs.maxBulletPerShootT; i++)
            {
                StartCoroutine(FireCoC());
            }

            if (gunTypeIdzs.shellFXx)
            {
                Vector2 shellPos = gunTypeIdzs.shellPointT.position;
                var _tempFX = SpawnSystemHelper.GetNextObject(gunTypeIdzs.shellFXx, true);
                _tempFX.transform.position = shellPos;
            }

            SoundManagerZS.PlaySfx(gunTypeIdzs.soundFireE, gunTypeIdzs.soundFireVolumeE);
            SoundManagerZS.PlaySfx(gunTypeIdzs.shellSoundD, gunTypeIdzs.shellSoundVolumeE);

            CancelInvoke(nameof(CheckBulletRemain));
            Invoke(nameof(CheckBulletRemain), gunTypeIdzs.rateE);

            if (gunTypeIdzs.dualShotT)
                Invoke(nameof(ShootSecondGun), gunTypeIdzs.fireSecondGunDelayY);
        }

        private void ShootSecondGun()
        {
            //gunTypeID.bullet--;
            //SubtractBullet(1);
            for (int i = 0; i < gunTypeIdzs.maxBulletPerShootT; i++)
            {
                StartCoroutine(FireCoC());
            }
            SoundManagerZS.PlaySfx(gunTypeIdzs.soundFireE, gunTypeIdzs.soundFireVolumeE);
            SoundManagerZS.PlaySfx(gunTypeIdzs.shellSoundD, gunTypeIdzs.shellSoundVolumeE);
        }

        public IEnumerator FireCoC()
        {
        
            yield return null;

            var _dir = (IsFacingRight ? Vector2.right : Vector2.left) + new Vector2(0, Random.Range(-(1f - gunTypeIdzs.accuracyY), (1f - gunTypeIdzs.accuracyY)));
            RaycastHit2D hit = Physics2D.Raycast(playerSpineHelperR.GetFireWorldPointT() + (IsFacingRight ? Vector2.left : Vector2.right), _dir, 100, targetLayer);

            if (gunTypeIdzs.muzzleTracerFXx)
            {
                var _tempFX = SpawnSystemHelper.GetNextObject(gunTypeIdzs.muzzleTracerFXx, true);
                _tempFX.transform.position = playerSpineHelperR.GetFireWorldPointT();
                _tempFX.transform.right = _dir;
            }

            if (gunTypeIdzs.muzzleFXx)
            {
                var _muzzle = SpawnSystemHelper.GetNextObject(gunTypeIdzs.muzzleFXx, playerSpineHelperR.GetFireWorldPointT(), true);

                _muzzle.transform.right = (IsFacingRight ? Vector2.right : Vector2.left);
                //_muzzle.transform.parent = firePoint;
            }

            if (hit)
            {
                var takeDamage = (ICanTakeDamage)hit.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
                if (takeDamage != null)
                {
                    var finalDamage = (int)(Random.Range(gunTypeIdzs.minPercentAffecT * 0.01f, 1f) * gunTypeIdzs.UpgradeRangeDamage);

                    takeDamage.TakeDamageE(finalDamage, Vector2.zero, hit.point, gameObject);
                }
            }

            if (gunTypeIdzs.reloadPerShootT)
            {
                StartCoroutine(ReloadGunSub());
            }
        }

        private void CheckBulletRemain()
        {
            if (gunTypeIdzs.Bullet <= 0)
            {
                GunManagerZS.Instance.NextGunN();
            }
        }

        public void ReloadGun()
        {
            SetStateT(GunHandlerState.RELOADING);
            //SoundManager.PlaySfx (soundReload, soundReloadVolume);
            AnimSetTrigger("reload");
            AnimSetBoolL("reloading", true);
            Invoke("ReloadComplete", gunTypeIdzs.reloadTimeE);

            SoundManagerZS.PlaySfx(gunTypeIdzs.reloadSoundD, gunTypeIdzs.reloadSoundVolumeE);
        }

        IEnumerator ReloadGunSub()
        {
            SetStateT(GunHandlerState.RELOADING);
            AnimSetBoolL("isReloadPerShootNeeded", true);

            yield return new WaitForSeconds(gunTypeIdzs.reloadTimeE);

            SetStateT(GunHandlerState.AVAILABLE);
            AnimSetBoolL("isReloadPerShootNeeded", false);
        }

        public void ReloadComplete()
        {
            lastTimeShootingG = Time.time;
            AnimSetBoolL("reloading", false);
            SetStateT(GunHandlerState.AVAILABLE);
        }

        public void ThrowGrenade()
        {
            var obj = (GameObject)SpawnSystemHelper.GetNextObject(grenade, false);
            SoundManagerZS.PlaySfx(throwGrenadeSound);
            obj.GetComponent<GrenadeZS>().SetDirection(IsFacingRight);
            obj.transform.position = throwPoint.position;
            obj.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isCollectItem = (ICanCollect)collision.GetComponent(typeof(ICanCollect));
            if (isCollectItem != null)
            {
                isCollectItem.CollectT();
            }
        }

        public void SetGun(GunTypeIDZS gunIdzs)
        {
            weaponState = WeaponState.GUN;
            anim.runtimeAnimatorController = gunIdzs.animatorOverrideE;
            gunTypeIdzs = gunIdzs;
            AnimSetTrigger("swap-gun");
            allowShootingG = false;
            SoundManagerZS.PlaySfx(SoundManagerZS.Instance.swapGun);
            Invoke(nameof(AllowShooting), 0.3f);
        }

        void AllowShooting()
        {
            allowShootingG = true;
        }

        #region MELEE ATTACK
        PlayerMeleeWeaponZS playerMeleeWeaponZs;

        public void SetMelee()
        {
            weaponState = WeaponState.MELEE;
            anim.runtimeAnimatorController = playerMeleeWeaponZs.animatorOverride;
            SoundManagerZS.PlaySfx(playerMeleeWeaponZs.soundSwap);
            AnimSetTrigger("swap-gun");
        }

        public void MeleeAttack()
        {
            if (weaponState == WeaponState.GUN)
            {
                SetMelee();
                return;
            }
            if (Time.time > (playerMeleeWeaponZs.lastAttackTime + playerMeleeWeaponZs.rate))
            {
                playerMeleeWeaponZs.lastAttackTime = Time.time;
                AnimSetTrigger("melee-attack");
            
                Invoke(nameof(MeleeCheckEnemy), playerMeleeWeaponZs.delayToSync);
            }
        }

        private void MeleeCheckEnemy()
        {
            SoundManagerZS.PlaySfx(playerMeleeWeaponZs.soundAttack);
            RaycastHit2D hit = Physics2D.CircleCast(playerMeleeWeaponZs.checkPoint.position, playerMeleeWeaponZs.radiusCheck, Vector2.zero, 0, targetLayer);

            if (hit)
            {
                var takeDamage = (ICanTakeDamage)hit.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
                if (takeDamage != null)
                {
                    var finalDamage = (int)(Random.Range(0.8f, 1f) * playerMeleeWeaponZs.damage);

                    takeDamage.TakeDamageE(finalDamage, new Vector2(5,0), hit.point, gameObject);
                }
            }
        }

        public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
        {
            currentHealthH -= damage;
            if (healthBarR)
                healthBarR.UpdateValueE(currentHealthH / (float)health);
            if (currentHealthH <= 0)
            {
                GameManagerZS.Instance.GameOver();
                AnimSetTrigger("dead");
                SoundManagerZS.PlaySfx(soundDie);
            }
            else
            {
                AnimSetTrigger("hurt");
                SoundManagerZS.PlaySfx(soundHurt);
            }
        }
        #endregion

        public void AddHearthH(int amount)
        {
            currentHealthH += amount;
            currentHealthH = Mathf.Min(currentHealthH, health);
            if (healthBarR)
                healthBarR.UpdateValueE(currentHealthH / (float)health);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, limitAbovePos, 0));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, limitBelowPos, 0));
        }
    }
}