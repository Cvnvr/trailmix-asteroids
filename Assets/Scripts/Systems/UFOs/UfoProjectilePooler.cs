using Zenject;

namespace Asteroids
{
    public class UfoProjectilePooler : BasePooler<Projectile>
    {
        [Inject] private DiContainer container;

        private WeaponData weaponData;
        
        protected override void Start()
        {
            // Do nothing as Init() handles prefilling
        }
        
        public void Init(WeaponData data, PoolData poolData)
        {
            weaponData = data;
            SetPoolData(poolData);
        }

        public void SpawnProjectile(ProjectileSpawnData data)
        {
            var projectile = Pop(data.Position, data.Rotation);
            projectile.SetProjectileData(weaponData.ProjectileData, data.Direction * weaponData.Speed);
        }

        protected override Projectile CreateObject()
        {
            return container.InstantiatePrefab(
                weaponData.ProjectileData.ProjectilePrefab, 
                transform.position, 
                transform.rotation, 
                null
            ).GetComponent<Projectile>(); 
        }
    }
}