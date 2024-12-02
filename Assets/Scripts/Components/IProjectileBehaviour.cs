using Entities.Projectiles;
using UnityEngine;

namespace Components
{
    public interface IProjectileBehaviour
    {
        void Init(Projectile projectile);
        void Update();
        void OnCollision(GameObject other);
    }
}