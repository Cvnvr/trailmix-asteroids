namespace Asteroids
{
    public class ProjectileBehaviourFactory : IProjectileBehaviourFactory
    {
        public BaseProjectileBehaviourComponent GetBoundComponent(Projectile projectile, ProjectileBehaviourData projectileBehaviourData)
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
                /*case WeaponSpawnAdditionalData spawnAdditionalData:
                    var spawnComponent = new ProjectileSpawnAdditionalComponent();
                    spawnComponent.Setup(projectile, spawnAdditionalData.SpawnedProjectileData, 
                        spawnAdditionalData.NumberToSpawn, spawnAdditionalData.SpawnOffset, spawnAdditionalData.SpawnDelay);
                    return spawnComponent;*/
                default:
                    return null;
            }
        }
    }
}