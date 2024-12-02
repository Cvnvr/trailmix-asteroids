using UnityEngine;

namespace Entities.Projectiles
{
    public class ProjectileDestroySelfAfterTimeComponent : BaseProjectileBehaviourComponent
    {
        private float lifetime;
        
        public void Setup(Projectile projectile, float lifetime)
        {
            Init(projectile);
            this.lifetime = lifetime;
        }

        public override void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                projectile.ReturnToPool();
            }
        }

        public override void OnCollision(GameObject other)
        {
        }
    }
}