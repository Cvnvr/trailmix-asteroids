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
        [SerializeField] private Projectile projectile;
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
            if (!canShoot)
                return;
            
            Pop();
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
            }
            shootDelayCoroutine = StartCoroutine(SetShootDelayCoroutine());
        }
        
        private IEnumerator SetShootDelayCoroutine()
        {
            canShoot = false;
            //yield return new WaitForSeconds(defaultShipProjectileData.Delay);
            yield return null;
            canShoot = true;
        }

        protected override Projectile CreateItem()
        {
            return Instantiate(projectile, shipNozzle.position, Quaternion.identity);
        }

        protected override void OnGet(Projectile item)
        {
            base.OnGet(item);
            
            item.transform.position = shipNozzle.position;
            item.transform.rotation = shipNozzle.rotation;
            
            item.Initialise(_ => Push(item));
            
            item.Fire(shipNozzle.up);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ShootInputEvent>(TryShoot);
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
                shootDelayCoroutine = null;
            }
        }
    }
}