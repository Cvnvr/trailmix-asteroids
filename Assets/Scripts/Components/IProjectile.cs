using Entities.Projectiles;

namespace Components
{
    public interface IProjectile
    {
        void SetData(ProjectileData projectileData);
        void Fire(UnityEngine.Vector2 velocity);
    }
}