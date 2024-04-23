using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    [RequireComponent(typeof(UnityEngine.UI.CanvasScaler))]
    public class CanvasCustomScaler : MonoBehaviour
    {
        private CanvasScaler canvasScaler;
        
        private void Awake()
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }

        private void Update()
        {
            if (IsTablet())
            {
                canvasScaler.matchWidthOrHeight = 0;
            }
            else
            {
                canvasScaler.matchWidthOrHeight = 1;
            }
        }

        private bool IsTablet()
        {
            // Calculate the aspect ratio
            float aspectRatio = (float)Screen.width / Screen.height;

            // Define common tablet aspect ratios
            float[] tabletAspectRatios = { 4f/3f, 16f/10f }; // Common tablet aspect ratios like 4:3 (iPad) and 16:10
            float[] phoneAspectRatios = { 16f/9f, 18.5f/9f, 19.5f/9f, 20f/9f }; // Common phone aspect ratios

            // Determine the closest common aspect ratio
            float minDifference = float.MaxValue;
            bool isTablet = false;

            // Check against tablet ratios
            foreach (float ratio in tabletAspectRatios)
            {
                float difference = Mathf.Abs(aspectRatio - ratio);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    isTablet = true; // This is closer to a tablet aspect ratio
                }
            }

            // Check against phone ratios
            foreach (float ratio in phoneAspectRatios)
            {
                float difference = Mathf.Abs(aspectRatio - ratio);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    isTablet = false; // This is closer to a phone aspect ratio
                }
            }

            return isTablet;
        }
        
    }
}
