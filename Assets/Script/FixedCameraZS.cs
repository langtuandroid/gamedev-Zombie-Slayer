using UnityEngine;

namespace Script
{
    public class FixedCameraZS : MonoBehaviour
    {
        [ReadOnly] public float fixedWidth;
        private readonly float orthographicSize = 3.8f;
   
        private void Start()
        {
            if (GameMode.Instance)
            {
                fixedWidth = orthographicSize * (GameMode.Instance.resolution.x / GameMode.Instance.resolution.y);
                Camera.main.orthographicSize = fixedWidth / (Camera.main.aspect);
            }
        }
    }
}
