using Entities.Projectiles;

namespace Systems.Projectiles
{
    public interface IProjectileBehaviourFactory
    {
        BaseProjectileBehaviourComponent BindTo(Projectile projectile, ProjectileBehaviourData projectileBehaviourData);
    }
}