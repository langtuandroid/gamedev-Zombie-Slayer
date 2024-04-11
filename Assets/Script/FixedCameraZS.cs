using UnityEngine;
using Zenject;

namespace Script
{
    public class FixedCameraZS : MonoBehaviour
    {
        [ReadOnly] public float fixedWidth;
        private readonly float orthographicSize = 3.8f;

        [Inject] private GameModeZS gameModeZs;
   
        private void Start()
        {
            if (gameModeZs)
            {
                fixedWidth = orthographicSize * (gameModeZs.resolution.x / gameModeZs.resolution.y);
                Camera.main.orthographicSize = fixedWidth / (Camera.main.aspect);
            }
        }
    }
}
