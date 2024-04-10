using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public enum BARREL_TYPE { Rewarded, Explosion}
    public class BarrelZS : MonoBehaviour,ICanTakeDamage
    {
        public BARREL_TYPE barrelType; 
        protected HealthBarEnemyNew healthBarR;
        public int health = 10;
        public Vector2 healthBarOffset = new Vector2(0, 1.5f);
        private int currentHealthH;

        public GameObject blowFX;
        public Vector2 blowOffset = new Vector2(0, 0.5f);
        public AudioClip blowSound, hitSound;

        [Header("REWARDED")]
        public int amount = 2;
        public GameObject[] spawnItem;

        [Header("EXPLOSION")]
        public float radius = 3;
        public float damage = 20;
        public LayerMask targetLayer;

        [ReadOnly] 
        public float activeDistance = 0;
        private Collider2D coll2Dd;
        private Animator animM;
        
        public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
        {
            currentHealthH -= (int)damage;

            if (healthBarR)
                healthBarR.UpdateValue(currentHealthH / (float)health);

            if (currentHealthH <= 0)
            {
                if (blowFX)
                    Instantiate(blowFX, transform.position + (Vector3) blowOffset, blowFX.transform.rotation);
                SoundManager.PlaySfx(blowSound);

                if(barrelType == BARREL_TYPE.Rewarded && spawnItem.Length>0)
                {
                    for(int i = 0; i < amount; i++)
                    {
                        Instantiate(spawnItem[Random.Range(0, spawnItem.Length)], transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
                    }
                }
                else if (barrelType == BARREL_TYPE.Explosion)
                {
                    var hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0, targetLayer);
                    if (hits.Length > 0)
                    {
                        foreach (var obj in hits)
                        {
                            obj.collider.gameObject.GetComponent<ICanTakeDamage>().TakeDamageE(damage, Vector2.zero, obj.point, gameObject);
                        }
                    }
                }
                gameObject.SetActive(false);
            }
            else
            {
                animM.SetTrigger("hit");
                SoundManager.PlaySfx(hitSound);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            animM = GetComponent<Animator>();
            var healthBarObj = (HealthBarEnemyNew)Resources.Load("HealthBar", typeof(HealthBarEnemyNew));
            healthBarR = (HealthBarEnemyNew)Instantiate(healthBarObj, healthBarOffset, Quaternion.identity);
            healthBarR.Init(transform, (Vector3)healthBarOffset);
            currentHealthH = health;
            activeDistance = 0.5f + Camera.main.aspect * Camera.main.orthographicSize;
            coll2Dd = GetComponent<Collider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            coll2Dd.enabled = Mathf.Abs(transform.position.x - Camera.main.transform.position.x) < activeDistance;
            healthBarR.transform.localScale = new Vector2(transform.localScale.x > 0 ? Mathf.Abs(healthBarR.transform.localScale.x) : -Mathf.Abs(healthBarR.transform.localScale.x), healthBarR.transform.localScale.y);
        }
    }
}