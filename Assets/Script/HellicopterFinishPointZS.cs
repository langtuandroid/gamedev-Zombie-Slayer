using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class HellicopterFinishPointZS : MonoBehaviour
{
    public static HellicopterFinishPointZS Instance;
    [FormerlySerializedAs("helliAnim")] public Animator helliAnimM;
    [FormerlySerializedAs("sign")] public GameObject signN;
    [FormerlySerializedAs("soundFX")] public AudioClip soundFXx;
    private bool isWorkingG = false;
    private bool isFireRocketT = false;
    private AudioSource audioSourceE;
    
    [HideInInspector]
    public bool isShowing = false;

    private void Awake()
    {
        Instance = this;
        isShowing = true;
        audioSourceE = gameObject.AddComponent<AudioSource>();
        audioSourceE.clip = soundFXx;
        audioSourceE.loop = true;
        audioSourceE.volume = 0;
        audioSourceE.Play();
    }

    private void Update()
    {
        if (GameManagerZS.Instance.state != GameManagerZS.GameState.Playing)
            return;

        if (isShowing)
        {
            float distanceToPlayer = Mathf.Abs(transform.position.x - GameManagerZS.Instance.player.transform.position.x);
            if (distanceToPlayer > 15)
                audioSourceE.volume = 0;
            else if (distanceToPlayer > 8)
                audioSourceE.volume = GlobalValueZS.IsSound ? 0.3f : 0;
            else
                audioSourceE.volume = GlobalValueZS.IsSound ? 0.8f : 0;
        }
    }

    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;
        audioSourceE.Pause();
        helliAnimM.SetBool("show", false);
    }

    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;
        audioSourceE.Play();
        audioSourceE.volume = GlobalValueZS.IsSound ? 0.8f : 0;
        StartCoroutine(FireRocketCoC());
        helliAnimM.SetBool("show", true);
    }

    private IEnumerator FireRocketCoC()
    {
        if (isFireRocketT)
            yield break;

        isFireRocketT = true;
        for(int i = 0; i < 3; i++)
        {
            RocketManager.Instance.FireRocket();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWorkingG)
        {
            if (collision.gameObject.GetComponent<BabeFollowerZS>())
            {
                collision.gameObject.SetActive(false);
            }
            return;
        }

        if(collision.gameObject == GameManagerZS.Instance.player.gameObject)
        {
             StartCoroutine(FireRocketCoC());
            isWorkingG = true;
            signN.SetActive(false);
            StartCoroutine(ProceedFinishLevelCoC());
        }
    }

    private IEnumerator ProceedFinishLevelCoC()
    {
        GameManagerZS.Instance.state = GameManagerZS.GameState.Success;
        GameManagerZS.Instance.player.gameObject.SetActive(false);


        var follower = FindObjectOfType<BabeFollowerZS>();
        if (follower != null)
        {
            follower.MoveToHelicopter(transform.position + Vector3.down * 0.5f);
            while (follower.gameObject.activeInHierarchy)
                yield return null;
        }


        yield return new WaitForSeconds(1);
        helliAnimM.SetTrigger("flyaway");
        yield return new WaitForSeconds(1);
        GameManagerZS.Instance.VictoryY();
        yield return new WaitForSeconds(1);
        audioSourceE.volume = 0;
    }
}