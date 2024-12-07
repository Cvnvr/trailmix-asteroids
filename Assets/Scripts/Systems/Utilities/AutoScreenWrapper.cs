using UnityEngine;
using Zenject;

namespace Asteroids
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

            if (position.y > screenBoundsCalculator.TopSidePadded)
            {
                newPosition.y = screenBoundsCalculator.BottomSidePadded;
            }
            else if (position.y < screenBoundsCalculator.BottomSidePadded)
            {
                newPosition.y = screenBoundsCalculator.TopSidePadded;
            }
            
            if (position.x > screenBoundsCalculator.RightSidePadded)
            {
                newPosition.x = screenBoundsCalculator.LeftSidePadded;
            }
            else if (position.x < screenBoundsCalculator.LeftSidePadded)
            {
                newPosition.x = screenBoundsCalculator.RightSidePadded;
            }

            transform.position = newPosition;
        }
    }
}