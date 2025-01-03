using System;

namespace Asteroids
{
    public class ProjectileBehaviourFactory : IProjectileBehaviourFactory
    {
        public IProjectileBehaviour GetBoundComponent(Projectile projectile, Action pushCallback, ProjectileBehaviourData projectileBehaviourData)
        {
            if (projectile == null)
                return null;
            
            switch (projectileBehaviourData)
            {
                case ProjectileDestroySelfAfterTimeData timeData:
                    var timeComponent = new ProjectileDestroySelfAfterTimeComponent();
                    timeComponent.Setup(pushCallback, timeData.Lifetime);
                    return timeComponent;
                case ProjectileDestroySelfAfterDistanceData distanceData:
                    var distanceComponent = new ProjectileDestroySelfAfterDistanceComponent();
                    distanceComponent.Setup(projectile, pushCallback, distanceData.Distance);
                    return distanceComponent;
                case ProjectileDestroySelfAfterCollisionData:
                    var collisionComponent = new ProjectileDestroySelfAfterCollisionComponent();
                    collisionComponent.Init(pushCallback);
                    return collisionComponent;
                case ProjectileDestroyOtherAfterCollisionData destroyOtherData:
                    var destroyOtherComponent = new ProjectileDestroyOtherAfterCollisionComponent();
                    destroyOtherComponent.Setup(pushCallback, destroyOtherData.TagsToIgnore);
                    return destroyOtherComponent;
                default:
                    return null;
            }
        }
    }
}