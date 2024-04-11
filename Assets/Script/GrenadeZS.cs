using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class GrenadeZS : MonoBehaviour
{
    [SerializeField] private float damageE = 100;
    [SerializeField] private float radiusS = 3;
    [SerializeField] private LayerMask targetLayerL;
    [SerializeField] private AudioClip soundD;
    [SerializeField] private Vector2 forceE = new Vector2(5, 10);
    [SerializeField] private float torqueForceE = 100;
    [SerializeField] private float offsetBlowYY = -1f;
    [SerializeField] private GameObject blowFX;

    private Rigidbody2D rigG;

    private float beginY;
    
    private void OnEnable()
    {
        beginY = transform.position.y;
        if(rigG == null)
            rigG = GetComponent<Rigidbody2D>();

        rigG.velocity = forceE;
        rigG.AddTorque(torqueForceE);
    }

    public void SetDirection(bool isFacingRight)
    {
        forceE.x = Mathf.Abs(forceE.x) * (isFacingRight ? 1 : -1);
    }
   
    private void Update()
    {
        if(transform.position.y < (beginY + offsetBlowYY))
        {
            var hits = Physics2D.CircleCastAll(transform.position, radiusS, Vector2.zero, 0, targetLayerL);
            if (hits.Length > 0)
            {
                foreach(var obj in hits)
                {
                    obj.collider.gameObject.GetComponent<ICanTakeDamage>().TakeDamageE(damageE, Vector2.zero, obj.point, gameObject);
                }
            }

            SoundManagerZS.PlaySfx(soundD);
            SpawnSystemHelper.GetNextObject(blowFX, true).transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusS);
    }
}
