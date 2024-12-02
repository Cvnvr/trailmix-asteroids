using Entities.Projectiles;
using Events.Input;
using Systems.Pooling;
using UnityEngine;
using Zenject;

namespace Systems.Projectiles
{
    public class ShipProjectilePooler : BasePooler<Projectile>
    {
        [SerializeField] private DefaultShipProjectileData defaultShipProjectileData;
        [SerializeField] private Transform shipNozzle;
        
        [Inject] private SignalBus signalBus;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ShootInputEvent>(SpawnProjectile);
        }

        private void SpawnProjectile()
        {
            var projectile = Pop();
            if (projectile)
            {
                projectile.transform.position = shipNozzle.position;
                projectile.transform.rotation = shipNozzle.rotation;
            }
        }

        protected override Projectile CreateItem()
        {
            var projectile = Instantiate(defaultShipProjectileData.Prefab, shipNozzle.position, Quaternion.identity);
            
            var projectileComponent = projectile.GetComponent<Projectile>();
            if (projectileComponent)
            {
                projectileComponent.Init(defaultShipProjectileData, _ => Push(projectileComponent));
            }
            
            return projectileComponent;
        }

        protected override void OnGet(Projectile item)
        {
            base.OnGet(item);
            
            item.Fire(shipNozzle.up);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ShootInputEvent>(SpawnProjectile);
        }
    }
}