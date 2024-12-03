using System;
using System.Collections;
using Entities.Projectiles;
using Events.Input;
using Systems.Pooling;
using UnityEngine;
using Zenject;

namespace Systems.Projectiles
{
    public class PlayerShooterController : BasePooler<Projectile>
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private ProjectileData projectileData;
        
        [SerializeField] private Transform shipNozzle;

        [Inject] private DiContainer container;
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
            
            Pop(shipNozzle.position, shipNozzle.rotation);
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
            }
            shootDelayCoroutine = StartCoroutine(SetShootDelayCoroutine());
        }
        
        private IEnumerator SetShootDelayCoroutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(projectileData.Delay);
            canShoot = true;
        }

        protected override Projectile CreateItem()
        {
            return container.InstantiatePrefab(projectile, shipNozzle.position, shipNozzle.rotation, transform).GetComponent<Projectile>();
        }

        protected override void ActivateObject(Projectile item)
        {
            base.ActivateObject(item);
            
            item.SetProjectileData(projectileData);
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

            Clear();
        }
    }
}