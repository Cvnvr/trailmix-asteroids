using UnityEngine;
using Zenject;

namespace Systems.Utilities
{
    public class AutoScreenWrapper : MonoBehaviour
    {
        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;

        private void Update()
        {
            if (!screenBoundsCalculator.IsInitialised)
                return;
            
            var position = transform.position;
            var newPosition = position;

            if (position.y > screenBoundsCalculator.TopSide)
            {
                newPosition.y = screenBoundsCalculator.BottomSide;
            }
            else if (position.y < screenBoundsCalculator.BottomSide)
            {
                newPosition.y = screenBoundsCalculator.TopSide;
            }
            
            if (position.x > screenBoundsCalculator.RightSide)
            {
                newPosition.x = screenBoundsCalculator.LeftSide;
            }
            else if (position.x < screenBoundsCalculator.LeftSide)
            {
                newPosition.x = screenBoundsCalculator.RightSide;
            }

            transform.position = newPosition;
        }
    }
}