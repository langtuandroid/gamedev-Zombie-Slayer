﻿using UnityEngine;
using System.Collections;
using Script;

public class SimpleProjectile : Projectile, ICanTakeDamage, IListener
{
    public int Damage = 30;
	public GameObject DestroyEffect;
    public int pointToGivePlayer = 100;
    public float timeToLive = 3;
	public Sprite newBulletImage;
	public AudioClip soundHitEnemy;
	[Range(0,1)]
	public float soundHitEnemyVolume = 0.5f;
	public AudioClip soundHitNothing;
	[Range(0,1)]
	public float soundHitNothingVolume = 0.5f;

	public GameObject ExplosionObj;
	private SpriteRenderer rend;

	public GameObject NormalFX;
	public GameObject DartFX;
	public GameObject destroyParent;
    float timeToLiveCounter = 0;
    void OnEnable()
    {
        timeToLiveCounter = timeToLive ;
    }
	void Start(){
		if (Explosion) {
			rend = GetComponent<SpriteRenderer> ();
//			rend.sprite = newBulletImage;
		}
		if(NormalFX)
		NormalFX.SetActive (!Explosion);
		if(DartFX)
		DartFX.SetActive (Explosion);
		GameManagerZS.Instance.Listeners.Add (this);
	}
	// Update is called once per frame

	bool comeBackToPlayer = false;
	void Update ()
	{
		if (isStop)
			return;

		if (destroyParent == null)
			destroyParent = gameObject;
		
		if ((timeToLiveCounter -= Time.deltaTime) <= 0) {
            
			if (Explosion && CanGoBackOwner)
				comeBackToPlayer = true;
			else
				DestroyProjectile ();
			
//			return;
		}

        if (comeBackToPlayer)
        {
            Vector3 comebackto = Owner.transform.position;
            destroyParent.transform.position = Vector2.MoveTowards(destroyParent.transform.position, comebackto, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, comebackto) < 0.26f)
                (destroyParent != null ? destroyParent : gameObject).SetActive(false);
        }
        else
        {
            //Debug.LogError((Direction + new Vector2(InitialVelocity.x, 0)) * Speed * Time.deltaTime + "?" + Speed);
            transform.Translate((Direction + new Vector2(InitialVelocity.x, 0)) * Speed * Time.deltaTime, Space.World);
        }
	}

	void DestroyProjectile(){
        if (DestroyEffect != null)
            SpawnSystemHelper.GetNextObject(DestroyEffect, true).transform.position = transform.position;

            //Instantiate (DestroyEffect, transform.position, Quaternion.identity);

		if (Explosion) {
			var bullet = Instantiate (ExplosionObj, transform.position, Quaternion.identity) as GameObject;
			//bullet.GetComponent<Grenade> ().DoExplosion (0);
		}

         (destroyParent != null ? destroyParent : gameObject).SetActive(false) ;
	}


	public void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE)
    {
		SoundManagerZS.PlaySfx (soundHitNothing, soundHitNothingVolume);
		DestroyProjectile ();
	}

	protected override void OnCollideOther (Collider2D other)
	{
//		other.gameObject.SendMessageUpwards ("TakeDamage", SendMessageOptions.DontRequireReceiver);
		SoundManagerZS.PlaySfx (soundHitNothing, soundHitNothingVolume);
		DestroyProjectile ();
	}

	protected override void OnCollideTakeDamageE (Collider2D other, ICanTakeDamage takedamage)
	{
		takedamage.TakeDamageE ((NewDamage == 0 ? Damage : NewDamage), Vector2.zero, transform.position, Owner, BODYPART.NONE,weaponEffect);
		SoundManagerZS.PlaySfx (soundHitEnemy, soundHitEnemyVolume);
		DestroyProjectile ();
	}

	bool isStop = false;
	#region IListener implementation

	public void IPlayY ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void ISuccessS ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void IPauseE ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void IUnPauseE ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void IGameOverR ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void IOnRespawnN ()
	{
		//		throw new System.NotImplementedException ();
	}

	public void IOnStopMovingOnN ()
	{
//		Debug.Log ("IOnStopMovingOn");
//		anim.enabled = false;
		isStop = true;
		//		GetComponent<Rigidbody2D> ().isKinematic = true;
	}

	public void IOnStopMovingOffF ()
	{
//		anim.enabled = true;
		isStop = false;
		//		GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	#endregion
}

