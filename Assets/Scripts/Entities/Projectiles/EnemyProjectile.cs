using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class EnemyProjectile : Projectile, IPlayerCollideable
    {
        [Inject] private SignalBus signalBus;
        
        public void OnPlayerCollision(GameObject player)
        {
            Destroy(player);
            signalBus.TryFire<PlayerDestroyedEvent>();
        }
    }
}