using Entities.Projectiles;
using Events.Input;
using Systems.Pooling;
using UnityEngine;
using Zenject;

namespace Systems.Projectiles
{
    public class ShipProjectilePooler : BasePooler<Projectile>
    {
        [SerializeField] private ProjectileData projectileData;
        [SerializeField] private Transform shipNozzle;
        
        [Inject] private SignalBus signalBus;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ShootInputEvent>(SpawnProjectile);
        }

        private void SpawnProjectile()
        {
            Pop();
        }

        protected override Projectile CreateItem()
        {
            var projectile = Instantiate(projectileData.Prefab, shipNozzle.position, Quaternion.identity);
            
            var projectileComponent = projectile.GetComponent<Projectile>();
            if (projectileComponent)
            {
                projectileComponent.Initialise(_ => Push(projectileComponent));
            }
            
            return projectileComponent;
        }

        protected override void OnGet(Projectile item)
        {
            base.OnGet(item);
            
            item.Fire(shipNozzle.up * projectileData.Speed);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ShootInputEvent>(SpawnProjectile);
        }
    }
}