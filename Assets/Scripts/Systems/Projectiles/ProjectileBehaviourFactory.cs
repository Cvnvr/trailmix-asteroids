using System;

namespace Asteroids
{
    public class ProjectileBehaviourFactory : IProjectileBehaviourFactory
    {
        public IProjectileBehaviour GetBoundComponent(Action pushCallback, ProjectileBehaviourData projectileBehaviourData)
        {
            switch (projectileBehaviourData)
            {
                case ProjectileDestroySelfAfterTimeData destroySelfAfterTime:
                    var timeComponent = new ProjectileDestroySelfAfterTimeComponent();
                    timeComponent.Setup(pushCallback, destroySelfAfterTime.Lifetime);
                    return timeComponent;
                case ProjectileDestroySelfAfterCollisionData:
                    var collisionComponent = new ProjectileDestroySelfAfterCollisionComponent();
                    collisionComponent.Init(pushCallback);
                    return collisionComponent;
                default:
                    return null;
            }
        }
    }
}