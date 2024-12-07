using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class RemoveIfLeaveScreenBounds : MonoBehaviour
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
            
            if (screenBoundsCalculator.IsOutsideScreenBounds(transform.position))
            {
                removable.RemoveSelf();
            }
        }
    }
}