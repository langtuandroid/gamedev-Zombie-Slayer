using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class AutoDestroyZS : MonoBehaviour {
        [FormerlySerializedAs("onlyDisactive")] [SerializeField] private bool onlyDisactiveE = false;
        [FormerlySerializedAs("destroyAfterTime")] [SerializeField] private float destroyAfterTimeE = 3f;

        private void OnEnable()
        {
            StartCoroutine(DisableCoO());
        }

        public void OnDisable()
        {
            CancelInvoke();
        }

        public void Init(float delay)
        {
            destroyAfterTimeE = delay;
        }

        private IEnumerator DisableCoO()
        {
            yield return null;

            yield return new WaitForSeconds(destroyAfterTimeE);
            if (onlyDisactiveE)
                gameObject.SetActive(false);
            else Destroy(gameObject);
        }
    }
}
