using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class CharacterZS : MonoBehaviour, IListener, ICanTakeDamage
{
    //ICanTakeDamage
    public virtual void TakeDamageE(float damage, Vector2 force, Vector2 hitPoint, GameObject instigator, BODYPART bodyPart = BODYPART.NONE, WeaponEffect weaponEffect = null, WEAPON_EFFECT forceEffect = WEAPON_EFFECT.NONE) { }

    public virtual void Hit() { }
    public virtual void Dead() { }
    public virtual void GetFreeze() { }
    public virtual void GetPosion() { }

    //IListener
    public virtual void IPlayY()
    {
    }

    public virtual void ISuccessS()
    {
    }

    public virtual void IPauseE()
    {
    }

    public virtual void IUnPauseE()
    {
    }

    public virtual void IGameOverR()
    {
    }

    public virtual void IOnRespawnN()
    {
    }

    public virtual void IOnStopMovingOnN()
    {
    }

    public virtual void IOnStopMovingOffF()
    {
    }
}
