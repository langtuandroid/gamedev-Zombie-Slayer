using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class ArrowProjectileZS : Projectile, IListener, ICanTakeDamage
    {
        [SerializeField] private Sprite hitImageBlood;
        [SerializeField] private SpriteRenderer arrowImageE;
        
        private Vector2 oldPosS;
        private int damageE = 30;
        
        [SerializeField] private GameObject destroyEffect;
        [SerializeField] private int pointToGivePlayer;
        [SerializeField] private float timeToLive = 3;
        [SerializeField] private AudioClip soundHitEnemy;
        [Range(0, 1)]
        [SerializeField] private float soundHitEnemyVolume = 0.5f;
        [SerializeField] private AudioClip soundHitNothing;
        [Range(0, 1)]
        [SerializeField] private float soundHitNothingVolume = 0.5f;

        [SerializeField] private GameObject explosionObj;
        private float timeToLiveCounterR = 0;
        [SerializeField] private bool parentToHitObjectT = true;

        private bool isHitT = false;
        private Rigidbody2D rigG;
        private bool criticalDamageE = false;
        private WeaponEffect arrowEffectT;
        private float disableAtYPosS = -1;
        private WEAPON_EFFECT forceEffectT;
        
        [SerializeField] private Vector2 checkTargetDistanceOffsetT = new Vector2(-0.25f,0);
        [SerializeField] private float checkTargetDistanceE = 1;
        
        private void OnEnable()
        {
            timeToLiveCounterR = timeToLive;
            isHitT = false;

            if (rigG == null)
                rigG = GetComponent<Rigidbody2D>();
            rigG.isKinematic = false;
        }

        public void Init(int damageMin, int damageMax, Vector2 velocityForce, float gravityScale, bool isCritical, WeaponEffect _arrowEffect, WEAPON_EFFECT _forceEffect = WEAPON_EFFECT.NONE, float _disableAtYPos = - 1)
        {
            arrowEffectT = _arrowEffect;
            forceEffectT = _forceEffect;
            damageE = Random.Range(damageMin, damageMax);
            criticalDamageE = isCritical;
            rigG = GetComponent<Rigidbody2D>();
            rigG.gravityScale = gravityScale;
            rigG.velocity = velocityForce;
            disableAtYPosS = _disableAtYPos;
        }

        private void Start()
        {
            oldPosS = transform.position;

            GameManagerZS.Instance.Listeners.Add(this);
        }
        

        // Update is called once per frame
        private void Update()
        {
            if (isHitT)
                return;

            if ((Vector2)transform.position != oldPosS)
            {
                transform.right = ((Vector2)transform.position - oldPosS).normalized;
            }

            //check hit target
            RaycastHit2D hit = Physics2D.Linecast(oldPosS, transform.position, LayerCollision);
            if (hit)
            {
                HitT(hit);
                isHitT = true;
            }

            oldPosS = transform.position;

            if ((timeToLiveCounterR -= Time.deltaTime) <= 0)
            {
                DestroyProjectileE();
            }

            if(disableAtYPosS != -1)
            {
                if (transform.position.y < disableAtYPosS)
                {
                    SoundManagerZS.PlaySfx(soundHitNothing, soundHitNothingVolume);
                    StartCoroutine(DestroyProjectileE(3));
                    isHitT = true;
                }
            }
            //check hit
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine((Vector2)transform.position + checkTargetDistanceOffsetT, (Vector2)transform.position + checkTargetDistanceOffsetT + (Vector2)transform.right * checkTargetDistanceE);
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
 
        }

        private void HitT(RaycastHit2D other)
        {
            transform.position = other.point + (Vector2)(transform.position - transform.Find("head").position);
        
            var takeDamage = (ICanTakeDamage)other.collider.gameObject.GetComponent(typeof(ICanTakeDamage));
            if (takeDamage != null)
            {
                OnCollideTakeDamageE(other.collider, takeDamage);
                if (criticalDamageE)
                    FloatingTextManager.Instance.ShowText("CRIT!", other.collider.gameObject.transform.position, Vector2.up, Color.yellow, 30);

            }
            else
                OnCollideOther(other.collider);
            //}
        }

        private IEnumerator DestroyProjectileE(float delay = 0)
        {
            var rig = GetComponent<Rigidbody2D>();
            rig.velocity = Vector2.zero;
            rig.isKinematic = true;

            yield return new WaitForSeconds(delay);
            if (destroyEffect != null)
                SpawnSystemHelper.GetNextObject(destroyEffect, true).transform.position = transform.position;

            if (Explosion)
            {
                var bullet = Instantiate(explosionObj, transform.position, Quaternion.identity) as GameObject;
            }

            gameObject.SetActive(false);
        }

        public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE)
        {
            SoundManagerZS.PlaySfx(soundHitNothing, soundHitNothingVolume);
            StartCoroutine(DestroyProjectileE(1));
        }

        protected override void OnCollideOther(Collider2D other)
        {
            SoundManagerZS.PlaySfx(soundHitNothing, soundHitNothingVolume);
            StartCoroutine(DestroyProjectileE(3));
            if (parentToHitObjectT)
                transform.parent = other.gameObject.transform;

        }

        protected override void OnCollideTakeDamageE(Collider2D other, ICanTakeDamage takedamage)
        {
            //Debug.LogError(other.name);
            base.OnCollideTakeDamageE(other, takedamage);

            takedamage.TakeDamageE(damageE, Vector2.zero, transform.position, Owner,BODYPART.NONE, arrowEffectT, forceEffectT);
            SoundManagerZS.PlaySfx(soundHitEnemy, soundHitEnemyVolume);
            StartCoroutine(DestroyProjectileE(0));

            if (parentToHitObjectT)
                transform.parent = other.gameObject.transform;

            if (arrowImageE && hitImageBlood)
            {
                arrowImageE.sprite = hitImageBlood;
            }
        }

        protected override void OnCollideTakeDamageBodyPartT(Collider2D other, ICanTakeDamageBodyPart takedamage)
        {
            base.OnCollideTakeDamageBodyPartT(other, takedamage);
            WeaponEffect weaponEffect = new WeaponEffect();
            takedamage.TakeDamage(damageE, force, transform.position, Owner);
            StartCoroutine(DestroyProjectileE(0));

            if (parentToHitObjectT)
                transform.parent = other.gameObject.transform;

            if (arrowImageE && hitImageBlood)
            {
                arrowImageE.sprite = hitImageBlood;
            }
        }

        bool isStop = false;
        #region IListener implementation

        public void IPlayY()
        {
            //		throw new System.NotImplementedException ();
        }

        public void ISuccessS()
        {
            //		throw new System.NotImplementedException ();
        }

        public void IPauseE()
        {
            //		throw new System.NotImplementedException ();
        }

        public void IUnPauseE()
        {
            //		throw new System.NotImplementedException ();
        }

        public void IGameOverR()
        {
            //		throw new System.NotImplementedException ();
        }

        public void IOnRespawnN()
        {
            //		throw new System.NotImplementedException ();
        }

        public void IOnStopMovingOnN()
        {
            //		Debug.Log ("IOnStopMovingOn");
            //		anim.enabled = false;
            isStop = true;
            //		GetComponent<Rigidbody2D> ().isKinematic = true;
        }

        public void IOnStopMovingOffF()
        {
            //		anim.enabled = true;
            isStop = false;
            //		GetComponent<Rigidbody2D> ().isKinematic = false;
        }

        public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
        {
            StartCoroutine(DestroyProjectileE(0));
        }

        #endregion
    }
}
