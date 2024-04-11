using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class PlayerSpineHelper : MonoBehaviour
    {
        [FormerlySerializedAs("firePointObj")]
        [Header("Fire Point object")]
        [SerializeField] private Transform firePointObjT;

        public Vector2 GetFireWorldPointT()
        {
            Vector3 _point;
            _point = firePointObjT.position;

            return _point;
        }
    }
}
