using UnityEngine;

namespace Systems.Utilities
{
    public class ScreenBoundsCalculator : MonoBehaviour
    {
        public bool IsInitialised => isInitialised;
        
        public float TopSide => screenBounds.y * screenPadding;
        public float BottomSide => -screenBounds.y * screenPadding;
        public float RightSide => screenBounds.x * screenPadding;
        public float LeftSide => -screenBounds.x * screenPadding;
        
        [SerializeField] private Camera screenCamera;
        [SerializeField] private float screenPadding = 1.15f;
        
        private bool isInitialised;
        private Vector2 screenBounds;

        private void Awake()
        {
            if (screenCamera == null)
            {
                Debug.LogWarning($"[{nameof(ScreenBoundsCalculator)}] No camera reference found. Attempting to use Camera.main.");
                screenCamera = Camera.main;
            }

            if (screenCamera == null)
            {
                Debug.LogError($"[{nameof(ScreenBoundsCalculator)}] Camera.main invalid. Screen bounds cannot be calculated.");
                isInitialised = false;
                return;
            }
            
            screenBounds = screenCamera.ScreenToWorldPoint(new Vector3(
                Screen.width,
                Screen.height,
                screenCamera.transform.position.z
                )
            );
            
            isInitialised = true;
        }
    }
}