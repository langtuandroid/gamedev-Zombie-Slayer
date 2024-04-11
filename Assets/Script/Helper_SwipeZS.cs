using UnityEngine;

namespace Script
{
    public class Helper_SwipeZS : MonoBehaviour
    {
        private float cameraLastPosS;
        [SerializeField] public float showHelperIfCameraIdle = 5;
        private Transform cameraMainN;
        private float lastMoveTimeE = 0;
        public GameObject helperObj;
        private bool isShownN = false;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("DontShowAgain", 0) == 1)
            {
                helperObj.SetActive(false);
                Destroy(this);
            }
        }
    
        private void Start()
        {
            cameraMainN = Camera.main.transform;
            cameraLastPosS = cameraMainN.position.x;
            InvokeRepeating(nameof(CheckingIdleE), 5, 0.1f);
            lastMoveTimeE = Time.time;
            helperObj.SetActive(false);
        }
        
        private void CheckingIdleE()
        {
            if (GameManagerZS.Instance.state == GameManagerZS.GameState.Playing)
            {
                if (cameraLastPosS != cameraMainN.position.x)
                {
                    cameraLastPosS = cameraMainN.position.x;
                    lastMoveTimeE = Time.time;
                    helperObj.SetActive(false);
                }
                else if (Time.time - lastMoveTimeE > showHelperIfCameraIdle)
                {
                    if (!isShownN)
                        helperObj.SetActive(true);
                    isShownN = true;
                }
            }else
                helperObj.SetActive(false);
        }

        public void DontShowAgainN()
        {
            PlayerPrefs.SetInt("DontShowAgain", 1);
            helperObj.SetActive(false);
            Destroy(this);
        }
    }
}
