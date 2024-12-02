using Entities.Projectiles;

namespace Systems.Projectiles
{
    public interface IProjectileBehaviourFactory
    {
        void BindTo(Projectile projectile, ProjectileBehaviourData projectileBehaviourData);
    }
}