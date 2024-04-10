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

    public GameObject LoadingObj;
    public Slider slider;
    public Text progressText;
    private IEnumerator LoadAsynchronouslyY(string name)
    {
        LoadingObj.SetActive(false);
        yield return new WaitForSeconds(delayY);
        LoadingObj.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int) progress * 100f + "%";
            //			Debug.LogError (progress);
            yield return null;
        }
    }
}
