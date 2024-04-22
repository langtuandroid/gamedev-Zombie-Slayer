using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FlashSceneZS : MonoBehaviour {

    public string sceneLoadD = "scene name";
    public float delayY = 1;
	
	private void Awake () {
        StartCoroutine(LoadAsynchronouslyY(sceneLoadD));
    }

    [SerializeField] private GameObject LoadingObjJ;
    [SerializeField] private Slider sliderR;
    [SerializeField] private TextMeshProUGUI progressTextT;
    
    private IEnumerator LoadAsynchronouslyY(string name)
    {
        LoadingObjJ.SetActive(false);
        yield return new WaitForSeconds(delayY);
        LoadingObjJ.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            sliderR.value = progress;
            progressTextT.text = (int) progress * 100f + "%";
            //			Debug.LogError (progress);
            yield return null;
        }
    }
}
