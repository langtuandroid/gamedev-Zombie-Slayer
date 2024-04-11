using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenUIZS : MonoBehaviour {
	public static BlackScreenUIZS Instance;
	private CanvasGroup canvasS;
	private Image imageE;
	// Use this for initialization
	private void Start () {
		Instance = this;
		canvasS = GetComponent<CanvasGroup> ();
		imageE = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	public void Show (float timer, Color _color) {
		imageE.color = _color;
		canvasS.alpha = 0;
		StartCoroutine (MMFadeZS.FadeCanvasGroup (GetComponent<CanvasGroup> (), timer, 1));
	}

	public void Show (float timer) {
		imageE.color = Color.black;
		canvasS.alpha = 0;
		StartCoroutine (MMFadeZS.FadeCanvasGroup (GetComponent<CanvasGroup> (), timer, 1));
	}

	public void Hide (float timer) {
		canvasS.alpha = 1;
		StartCoroutine (MMFadeZS.FadeCanvasGroup (GetComponent<CanvasGroup> (), timer, 0));
	}
}
