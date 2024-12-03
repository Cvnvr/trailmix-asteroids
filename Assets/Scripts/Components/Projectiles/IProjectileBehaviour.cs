using UnityEngine;

namespace Asteroids
{
    public interface IProjectileBehaviour
    {
        void Init(Projectile projectile);
        void Update();
        void OnCollision(GameObject other);
    }
}