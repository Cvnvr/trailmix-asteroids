using Entities.Projectiles;

namespace Systems.Projectiles
{
    public interface IProjectileBehaviourFactory
    {
        BaseProjectileBehaviourComponent GetBoundComponent(Projectile projectile, ProjectileBehaviourData projectileBehaviourData);
    }
}