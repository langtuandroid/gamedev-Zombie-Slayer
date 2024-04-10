using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class BabeFollowerZS : MonoBehaviour, ICanTakeDamage
    {
        [Header("SET UP")]
        [SerializeField] private Animator animM;
        [SerializeField] private float moveSpeedD = 10;
        [SerializeField] private float stopDistanceE = 2;
        [SerializeField] private float limitAbovePos = -0.8f;
        [SerializeField] private float limitBelowPos = -3.65f;
        [SerializeField] private AudioClip soundHurtT;
        [SerializeField] private AudioClip soundDieE;

        [Header("HEALTH")]
        [Range(0, 5000)]
        [SerializeField] private int healthH = 100;
        [SerializeField] private Vector2 healthBarOffsetT = new Vector2(0, 1.5f);

        private float currentHealthH;
        protected HealthBarEnemyNew HealthBar;
        private bool IsFacingRight { get { return transform.rotation.eulerAngles.y == 0; } }

        private void Start()
        {
            if (animM == null)
                animM = GetComponent<Animator>();

            currentHealthH = healthH;
            var healthBarObj = (HealthBarEnemyNew)Resources.Load("HealthBar", typeof(HealthBarEnemyNew));
            HealthBar = (HealthBarEnemyNew)Instantiate(healthBarObj, healthBarOffsetT, Quaternion.identity);

            HealthBar.Init(transform, (Vector3)healthBarOffsetT);
        }
        Vector2 velocity;
        bool isMoving = false;
        public  bool moveToHelicopter = false;
        public Vector3 helicopterPos;

        void Update()
        {
            if (!moveToHelicopter && !isMoving && Vector2.Distance(transform.position, GameManagerZS.Instance.player.transform.position) > stopDistanceE + 0.5f)
                isMoving = true;

            if (isMoving)
            {
                var dir = (moveToHelicopter? helicopterPos : GameManagerZS.Instance.player.transform.position) - transform.position;
                velocity = dir.normalized * moveSpeedD;
                transform.Translate(velocity * Time.deltaTime, Space.World);

                if ((transform.position.x > GameManagerZS.Instance.player.transform.position.x && IsFacingRight) || (transform.position.x < GameManagerZS.Instance.player.transform.position.x && !IsFacingRight))
                    Flip();

                if (moveToHelicopter)
                {
                    if (Vector2.Distance(transform.position, helicopterPos) < 0.2f)
                        gameObject.SetActive(false);
                }
                else if (Vector2.Distance(transform.position, GameManagerZS.Instance.player.transform.position) < stopDistanceE)
                    isMoving = false;
            }
            animM.SetBool("isMoving", isMoving);
        }

        public void MoveToHelicopter(Vector2 pos)
        {
            isMoving = true;
            helicopterPos = pos;
            moveToHelicopter = true;
        }

        void Flip()
        {
            transform.rotation = Quaternion.Euler(0, IsFacingRight ? 180 : 0, 0);
        }

        public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
        {
            currentHealthH -= damage;
            if (HealthBar)
                HealthBar.UpdateValue(currentHealthH / (float)healthH);
            if (currentHealthH <= 0)
            {
                GameManagerZS.Instance.GameOver();
                animM.SetTrigger("dead");
                SoundManager.PlaySfx(soundDieE);
            }
            else
            {
                animM.SetTrigger("hurt");
                SoundManager.PlaySfx(soundHurtT);
            }
        }
    }
}
