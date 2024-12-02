using System.Collections;
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

        private bool canShoot = true;
        
        private Coroutine shootDelayCoroutine;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ShootInputEvent>(TryShoot);
        }
        
        private void TryShoot()
        {
            if (canShoot)
            {
                Pop();
                
                if (shootDelayCoroutine != null)
                {
                    StopCoroutine(shootDelayCoroutine);
                }
                shootDelayCoroutine = StartCoroutine(ShootDelayCoroutine());
            }
        }
        
        private IEnumerator ShootDelayCoroutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(defaultShipProjectileData.Delay);
            canShoot = true;
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
            
            item.transform.position = shipNozzle.position;
            item.transform.rotation = shipNozzle.rotation;
            
            item.Fire(shipNozzle.up);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ShootInputEvent>(TryShoot);
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
            }
        }
    }
}