using UnityEngine;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileDestroySelfAfterCollision : BaseProjectileBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (projectile != null)
            {
                projectile.ReturnToPool();
            }
        }
    }
}