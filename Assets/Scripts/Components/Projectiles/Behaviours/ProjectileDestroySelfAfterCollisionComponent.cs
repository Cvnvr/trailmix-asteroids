using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroySelfAfterCollisionComponent : BaseProjectileBehaviourComponent
    {
        public override void Update()
        {
        }

        public override void OnCollision(GameObject collision)
        {
            if (projectile != null)
            {
                projectile.ReturnToPool();
            }
        }
    }
}