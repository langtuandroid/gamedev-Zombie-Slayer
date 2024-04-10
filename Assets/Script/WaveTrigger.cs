using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class WaveTrigger : MonoBehaviour, IListener
{
    public bool beginOnStart = false;
    public enum CALL_HELICOPTER { No, OnStart, OnEnd}
    public CALL_HELICOPTER callHelicopter;
    public EnemyWave enemyWave;
    [Header("Limit Camera Optional")]
    public bool useLimitOption = false;
    public float limitLeft = 8;
    public float limitRight = 4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManagerZS.Instance.player.gameObject)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        LevelEnemyManager.Instance.BeginWave(enemyWave);

        if (useLimitOption)
        {
            CameraFollowZS.Instance.TempLimitCameraA(transform.position.x - limitLeft, transform.position.x + limitRight);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (useLimitOption)
        {
            Gizmos.DrawWireCube(new Vector2(((transform.position.x - limitLeft) + (transform.position.x + limitRight))*0.5f, transform.position.y), new Vector2(limitRight + limitLeft, 1));
        }
    }

    public void IPlayY()
    {
        if (beginOnStart)
            SpawnEnemy();
    }

    public void ISuccessS()
    {
    }

    public void IPauseE()
    {
    }

    public void IUnPauseE()
    {
    }

    public void IGameOverR()
    {
    }

    public void IOnRespawnN()
    {
    }

    public void IOnStopMovingOnN()
    {
    }

    public void IOnStopMovingOffF()
    {
    }
}
