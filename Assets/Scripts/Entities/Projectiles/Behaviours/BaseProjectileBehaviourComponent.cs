using Components;
using UnityEngine;

namespace Entities.Projectiles
{
    public abstract class BaseProjectileBehaviourComponent : IProjectileBehaviour
    {
        protected Projectile projectile;
        
        public virtual void Init(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public abstract void Update();

        public abstract void OnCollision(GameObject collision);
    }
}