using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class HellicopterFinishPoint : MonoBehaviour
{
    public static HellicopterFinishPoint Instance;
    public Animator helliAnim;
    public GameObject sign;
    public AudioClip soundFX;
    bool isWorking = false;
    bool isFireRocket = false;
    AudioSource audioSource;
    [HideInInspector]
    public bool isShowing = false;

    private void Awake()
    {
        Instance = this;
        isShowing = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundFX;
        audioSource.loop = true;
        audioSource.volume = 0;
        audioSource.Play();
    }

    private void Start()
    {
      
    }

    private void Update()
    {
        if (GameManagerZS.Instance.state != GameManagerZS.GameState.Playing)
            return;

        if (isShowing)
        {
            float distanceToPlayer = Mathf.Abs(transform.position.x - GameManagerZS.Instance.player.transform.position.x);
            if (distanceToPlayer > 15)
                audioSource.volume = 0;
            else if (distanceToPlayer > 8)
                audioSource.volume = GlobalValueZS.IsSound ? 0.3f : 0;
            else
                audioSource.volume = GlobalValueZS.IsSound ? 0.8f : 0;
        }
    }

    public void Hide()
    {
        if (!isShowing)
            return;

        isShowing = false;
        audioSource.Pause();
        helliAnim.SetBool("show", false);
    }

    public void Show()
    {
        if (isShowing)
            return;

        isShowing = true;
        audioSource.Play();
        audioSource.volume = GlobalValueZS.IsSound ? 0.8f : 0;
        StartCoroutine(FireRocketCo());
        helliAnim.SetBool("show", true);
    }

    IEnumerator FireRocketCo()
    {
        if (isFireRocket)
            yield break;

        isFireRocket = true;
        for(int i = 0; i < 3; i++)
        {
            RocketManager.Instance.FireRocket();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWorking)
        {
            if (collision.gameObject.GetComponent<BabeFollowerZS>())
            {
                collision.gameObject.SetActive(false);
            }
            return;
        }

        if(collision.gameObject == GameManagerZS.Instance.player.gameObject)
        {
             StartCoroutine(FireRocketCo());
            isWorking = true;
            sign.SetActive(false);
            StartCoroutine(ProceedFinishLevelCo());
        }
    }

    IEnumerator ProceedFinishLevelCo()
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
        helliAnim.SetTrigger("flyaway");
        yield return new WaitForSeconds(1);
        GameManagerZS.Instance.VictoryY();
        yield return new WaitForSeconds(1);
        audioSource.volume = 0;
    }
}