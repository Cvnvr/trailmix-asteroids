using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class RemoveSelfIfLeaveScreenBounds : MonoBehaviour
    {
        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;

        private IRemovable removable;
        
        private void Awake()
        {
            removable = GetComponent<IRemovable>();
        }
        
        private void Update()
        {
            if (removable == null)
                return;
            
            if (screenBoundsCalculator.IsOutsidePaddedScreenBounds(transform.position))
            {
                removable.RemoveSelf();
            }
        }
    }
}