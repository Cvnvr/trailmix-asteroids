using UnityEngine;

namespace Entities.Projectiles
{
    public abstract class BaseProjectileBehaviour : MonoBehaviour, IProjectileBehaviour
    {
        protected Projectile projectile;
        
        public virtual void Init(Projectile projectile)
        {
            this.projectile = projectile;
        }
    }
}