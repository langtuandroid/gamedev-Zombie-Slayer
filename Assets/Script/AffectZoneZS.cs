using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class AffectZoneZS : MonoBehaviour
    {
        [FormerlySerializedAs("isActived")] [ReadOnly] public bool isActivated = false;

        [Header("LIGHTNING")]
        [SerializeField] private float lightingActiveTime = 3;
        [SerializeField] private float lightingDamage = 10;
        [SerializeField] private GameObject lightingFX;
        [SerializeField] private float lightingRate = 1;
        [SerializeField] private AudioClip lightingSound;

        [Header("FROZEN")]
        [SerializeField] private float frozenActiveTime = 3;
        [SerializeField] private float frozenAffectTime = 3;
        [SerializeField] private float frozenDamage = 10;
        [SerializeField] private GameObject frozenFX;
        [FormerlySerializedAs("forzenSound")] [SerializeField] private AudioClip frozenSound;

        [Header("POISON")]
        [SerializeField] private float poisonActiveTime = 3;
        [SerializeField] private float poisonAffectTime = 3;
        [SerializeField] private float poisonDamage = 10;
        [SerializeField] private GameObject poisonFX;
        [SerializeField] private float poisonRate = 0.5f;
        [SerializeField] private AudioClip poisonSound;

        // Start is called before the first frame update
        private List<Enemy> listEnemyInZoneE;
        private AffectZoneType zoneTypeE;
        private Animator animM;

        private void Awake()
        {
            animM = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            listEnemyInZoneE = new List<Enemy>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            isActivated = false;
        }

        public void ActiveE(AffectZoneType _type)
        {
            if (!isActivated)
            {
                zoneTypeE = _type;
                StartCoroutine(ActiveCo());
                switch (zoneTypeE)
                {
                    case AffectZoneType.Lighting:
                        StartCoroutine(StopActiveCo());
                        break;
                    case AffectZoneType.Poison:
                        StartCoroutine(StopActiveCo());
                        break;
                }
           
            }
        }

        IEnumerator ActiveCo()
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            yield return null;
            isActivated = true;
            if (animM)
                animM.SetBool("isActivating", true);
            while (true)
            {
                if (listEnemyInZoneE.Count > 0)
                {
                    List<Enemy> _tempList = new List<Enemy>(listEnemyInZoneE);
                    foreach (var target in _tempList)
                    {
                        if (target.gameObject != null)
                        {
                            switch (zoneTypeE)
                            {
                                case AffectZoneType.Lighting:
                                    var _weaponFX = new WeaponEffect();
                                    _weaponFX.effectType = WEAPON_EFFECT.LIGHTING;
                                    target.TakeDamageE(lightingDamage, Vector2.zero, target.gameObject.transform.position, gameObject, BODYPART.NONE, _weaponFX);
                                    if (lightingFX)
                                        SpawnSystemHelper.GetNextObject(lightingFX, true).transform.position = target.gameObject.transform.position;
                                    SoundManagerZS.PlaySfx(lightingSound);
                                    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
                                    break;
                                case AffectZoneType.Frozen:
                                    target.TakeDamageE(frozenDamage, Vector2.zero, target.gameObject.transform.position, gameObject);
                                    target.Freeze(frozenAffectTime, gameObject);
                                    if (frozenFX)
                                    {
                                        var _fx = SpawnSystemHelper.GetNextObject(frozenFX, true);
                                        _fx.GetComponent<AutoDestroyZS>().Init(frozenAffectTime);
                                        _fx.transform.position = target.gameObject.transform.position;
                                    }
                                    SoundManagerZS.PlaySfx(frozenSound);
                                    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
                                    break;
                                case AffectZoneType.Poison:
                                    target.Poison(poisonDamage, poisonActiveTime, gameObject);
                                    if (poisonFX)
                                    {
                                        var _fx = SpawnSystemHelper.GetNextObject(poisonFX, true);
                                        _fx.GetComponent<AutoDestroyZS>().Init(poisonAffectTime);
                                        _fx.transform.position = target.gameObject.transform.position;
                                    }
                                    SoundManagerZS.PlaySfx(poisonSound);
                                    break;
                            }
                        }
                    }
                }

                switch (zoneTypeE)
                {
                    case AffectZoneType.Lighting:
                        yield return new WaitForSeconds(lightingRate);
                        break;
                    case AffectZoneType.Frozen:
                        Stop();
                        break;
                    case AffectZoneType.Poison:
                        yield return new WaitForSeconds(poisonRate);
                        break;
                }
                yield return null;
            }
        }

        IEnumerator StopActiveCo()
        {
            float delay = 0;
            switch(zoneTypeE)
            {
                case AffectZoneType.Lighting:
                    delay = lightingActiveTime;
                    break;
                case AffectZoneType.Frozen:
                    delay = frozenActiveTime;
                    break;
                case AffectZoneType.Poison:
                    delay = poisonActiveTime;
                    break;
            }
            yield return new WaitForSeconds(delay);

            Stop();
        }

        void Stop()
        {
            AffectZoneManagerZS.Instance.FinishAffectT();
            StopAllCoroutines();
            isActivated = false;
            if (animM)
                animM.SetBool("isActivating", false);
            gameObject.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (!listEnemyInZoneE.Contains(enemy))
                        listEnemyInZoneE.Add(enemy);
                }
            }
            //Debug.LogError(collision.gameObject + "list: " + listEnemyInZone.Count);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                    listEnemyInZoneE.Remove(enemy);
            }
            //Debug.LogError(collision.gameObject);
        }
    }
}
