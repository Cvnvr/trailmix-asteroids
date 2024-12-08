using UnityEngine;

namespace Asteroids
{
    public class ScreenBoundsCalculator : MonoBehaviour
    {
        public bool IsInitialised => isInitialised;
        
        public float TopSide => screenBounds.y;
        public float RightSide => screenBounds.x;
        public float BottomSide => -screenBounds.y;
        public float LeftSide => -screenBounds.x;

        public float TopSidePadded => screenBounds.y * outerScreenPadding;
        public float RightSidePadded => screenBounds.x * outerScreenPadding;
        public float BottomSidePadded => -screenBounds.y * outerScreenPadding;
        public float LeftSidePadded => -screenBounds.x * outerScreenPadding;
        
        [SerializeField] private Camera screenCamera;
        [SerializeField] private float outerScreenPadding = 1.15f;
        
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
            
            return position.x > RightSide || 
                   position.x < LeftSide || 
                   position.y > TopSide || 
                   position.y < BottomSide;
        }
        
        public bool IsOutsideOffScreenBounds(Vector2 position)
        {
            if (!isInitialised)
                return false;
            
            return position.x > RightSidePadded || 
                   position.x < LeftSidePadded || 
                   position.y > TopSidePadded || 
                   position.y < BottomSidePadded;
        }
        
        public Vector2 GetRandomOffScreenPosition()
        {
            var xPos = 0f;
            var yPos = 0f;
            
            var edge = Random.Range(0, 4);
            switch (edge)
            {
                case 0: // Top edge
                    xPos = Random.Range(LeftSidePadded, RightSidePadded);
                    yPos = TopSidePadded;
                    break;
                case 1: // Bottom edge
                    xPos = Random.Range(LeftSidePadded, RightSidePadded);
                    yPos = BottomSidePadded;
                    break;
                case 2: // Left edge
                    xPos = LeftSidePadded;
                    yPos = Random.Range(BottomSidePadded, TopSidePadded);
                    break;
                case 3: // Right edge
                    xPos = RightSidePadded;
                    yPos = Random.Range(BottomSidePadded, TopSidePadded);
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