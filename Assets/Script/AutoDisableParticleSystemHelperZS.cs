using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AutoDisableParticleSystemHelperZS : MonoBehaviour
    {
        [FormerlySerializedAs("OnlyDeactivate")] public bool onlyDeactivate = true;

        private void OnEnable()
        {
            StartCoroutine(nameof(CheckAlive));
        }

        private IEnumerator CheckAlive()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (!GetComponent<ParticleSystem>().IsAlive(true))
                {
                    if (onlyDeactivate)
                    {
                        this.gameObject.SetActive(false);
                    }
                    else
                        GameObject.Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
