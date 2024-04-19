using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FlashSceneZS : MonoBehaviour {

	[FormerlySerializedAs("sceneLoad")] public string sceneLoadD = "scene name";
	[FormerlySerializedAs("delay")] public float delayY = 2;
	
	private void Awake () {
        StartCoroutine(LoadAsynchronouslyY(sceneLoadD));
    }

    [FormerlySerializedAs("LoadingObj")] [SerializeField] private GameObject LoadingObjJ;
    [SerializeField] private Slider sliderR;
    [FormerlySerializedAs("progressText")] [SerializeField] private Text progressTextT;
    
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
