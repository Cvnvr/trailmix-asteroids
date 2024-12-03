using Entities.Projectiles;

namespace Components
{
    public interface IProjectile
    {
        void SetProjectileData(ProjectileData projectileData);
        void Fire(UnityEngine.Vector2 velocity);
    }
}