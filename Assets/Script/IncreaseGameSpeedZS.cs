using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script
{
    public class IncreaseGameSpeedZS : MonoBehaviour
    {
        [FormerlySerializedAs("timeSpeedUp")] [SerializeField] private float timeSpeedUpP = 2;
        [FormerlySerializedAs("blinkingObj")] [SerializeField] private GameObject blinkingObjJ;
        [FormerlySerializedAs("speedTxt")] [SerializeField] private Text speedTxtT;
        [FormerlySerializedAs("helperObj")] [SerializeField] private GameObject helperObjJ;
        
        private void Start()
        {
            speedTxtT.text = "Speed x1";
            helperObjJ.SetActive(false);
            Invoke(nameof(ShowHelperR), 10);
        }

        private void ShowHelperR()
        {
            if (PlayerPrefs.GetInt("IncreaseGameSpeedDontShowAgain", 0) == 0)
                helperObjJ.SetActive(true);
        }

        public void ChangeSpeedD()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = timeSpeedUpP;
                StartCoroutine(BlinkingCoC());
                speedTxtT.text = "Speed x" + timeSpeedUpP;
                //SoundManager.PlaySfx(SoundManager.Instance.soundTimeUp);
            }
            else
            {
                blinkingObjJ.SetActive(true);
                Time.timeScale = 1;
                StopAllCoroutines();
                speedTxtT.text = "Speed x1";
                //SoundManager.PlaySfx(SoundManager.Instance.soundTimeDown);
            }

            PlayerPrefs.SetInt("IncreaseGameSpeedDontShowAgain", 1);
            helperObjJ.SetActive(false);
        }

        private IEnumerator BlinkingCoC()
        {
            while (true)
            {
                blinkingObjJ.SetActive(true);
                yield return new WaitForSeconds(0.8f);
                blinkingObjJ.SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
