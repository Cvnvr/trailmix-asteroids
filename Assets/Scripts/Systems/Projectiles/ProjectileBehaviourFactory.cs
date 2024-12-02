using Entities.Projectiles;

namespace Systems.Projectiles
{
    public class ProjectileBehaviourFactory : IProjectileBehaviourFactory
    {
        public void BindTo(Projectile projectile, ProjectileBehaviourData projectileBehaviourData)
        {
            if (projectile == null)
                return;
            
            switch (projectileBehaviourData)
            {
                case ProjectileDestroySelfAfterTimeData destroySelfAfterTime:
                    var timeComponent = projectile.gameObject.AddComponent<ProjectileDestroySelfAfterTimeComponent>();
                    timeComponent.Setup(projectile, destroySelfAfterTime.Lifetime);
                    break;
                case ProjectileDestroySelfAfterCollisionData:
                    var collisionComponent = projectile.gameObject.AddComponent<ProjectileDestroySelfAfterCollisionComponent>();
                    collisionComponent.Init(projectile);
                    break;
            }
        }
    }
}