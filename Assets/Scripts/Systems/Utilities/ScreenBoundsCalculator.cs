using UnityEngine;

namespace Asteroids
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
            
            screenBounds = screenCamera.ScreenToWorldPoint(new Vector2(
                Screen.width,
                Screen.height
                )
            );
            
            isInitialised = true;
        }
        
        public bool IsOutsideScreenBounds(Vector2 position)
        {
            if (!isInitialised)
                return false;
            
            return position.x > RightSide || position.x < LeftSide || position.y > TopSide || position.y < BottomSide;
        }
        
        public Vector2 GetRandomOffScreenPosition()
        {
            var xPos = 0f;
            var yPos = 0f;
            
            var edge = Random.Range(0, 4);
            switch (edge)
            {
                case 0: // Top edge
                    xPos = Random.Range(LeftSide, RightSide);
                    yPos = TopSide;
                    break;
                case 1: // Bottom edge
                    xPos = Random.Range(LeftSide, RightSide);
                    yPos = BottomSide;
                    break;
                case 2: // Left edge
                    xPos = LeftSide;
                    yPos = Random.Range(BottomSide, TopSide);
                    break;
                case 3: // Right edge
                    xPos = RightSide;
                    yPos = Random.Range(BottomSide, TopSide);
                    break;
                default:
                    xPos = 0f;
                    yPos = 0f;
                    break;
            }

            return new Vector2(xPos, yPos);
        }

        public Vector2 GetCenterOfScreen()
        {
            if (!isInitialised)
                return Vector2.zero;
            
            return screenCamera.ScreenToWorldPoint(new Vector2((float)Screen.width / 2, (float)Screen.height / 2));
        }
    }
}