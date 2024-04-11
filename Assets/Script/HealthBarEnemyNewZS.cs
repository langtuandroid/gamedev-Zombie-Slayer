using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBarEnemyNewZS : MonoBehaviour {
	[FormerlySerializedAs("healthBar")] [SerializeField] private Transform healthBarR;
	[FormerlySerializedAs("showTime")] [SerializeField] private float showTimeE = 1f;
	[FormerlySerializedAs("hideSpeed")] [SerializeField] private float hideSpeedD = 0.5f;

	[SerializeField] private SpriteRenderer backgroundImage;
	[SerializeField] private SpriteRenderer barImage;

	private Color oriBgImage, oriBarImageВ;

    private Transform targetT;
    private Vector3 offsetT;
    
	private void Start () {
		healthBarR.localScale = new Vector2 (1, healthBarR.localScale.y);
		oriBgImage = backgroundImage.color;
		oriBarImageВ = barImage.color;

		//hide all
		backgroundImage.color = new Color (oriBgImage.r, oriBgImage.g, oriBgImage.b, 0);
		barImage.color = new Color (oriBarImageВ.r, oriBarImageВ.g, oriBarImageВ.b, 0);
	}

    public void Init(Transform _target, Vector3 _offset)
    {
        targetT = _target;
        offsetT = _offset;
    }

    private void Update()
    {
        if (targetT)
        {
            transform.position = targetT.position + offsetT;
        }
    }

    public void UpdateValueE(float value){
		StopAllCoroutines ();
		CancelInvoke ();

		backgroundImage.color = oriBgImage;
		barImage.color = oriBarImageВ;

		value = Mathf.Max (0, value);
		healthBarR.localScale = new Vector2 (value, healthBarR.localScale.y);
		if (value > 0)
			Invoke (nameof(HideBarR), showTimeE);
		else
			gameObject.SetActive (false);
	}

	private void HideBarR(){
		StartCoroutine (MMFadeZS.FadeSpriteRenderer (backgroundImage, hideSpeedD, new Color (oriBgImage.r, oriBgImage.g, oriBgImage.b, 0)));
		StartCoroutine (MMFadeZS.FadeSpriteRenderer (barImage, hideSpeedD, new Color (oriBarImageВ.r, oriBarImageВ.g, oriBarImageВ.b, 0)));
	}
}
