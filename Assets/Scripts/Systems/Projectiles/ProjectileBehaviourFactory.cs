using Entities.Projectiles;

namespace Systems.Projectiles
{
    public class ProjectileBehaviourFactory : IProjectileBehaviourFactory
    {
        public BaseProjectileBehaviourComponent BindTo(Projectile projectile, ProjectileBehaviourData projectileBehaviourData)
        {
            if (projectile == null)
                return null;
            
            switch (projectileBehaviourData)
            {
                case ProjectileDestroySelfAfterTimeData destroySelfAfterTime:
                    var timeComponent = new ProjectileDestroySelfAfterTimeComponent();
                    timeComponent.Setup(projectile, destroySelfAfterTime.Lifetime);
                    return timeComponent;
                case ProjectileDestroySelfAfterCollisionData:
                    var collisionComponent = new ProjectileDestroySelfAfterCollisionComponent();
                    collisionComponent.Init(projectile);
                    return collisionComponent;
                default:
                    return null;
            }
        }
    }
}