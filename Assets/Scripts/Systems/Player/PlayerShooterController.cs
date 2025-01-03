using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class PlayerShooterController : BasePooler<Projectile>
    {
        [SerializeField] private WeaponData defaultWeaponData;
        [SerializeField] private Transform shipNozzle;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        [Inject] private IWeaponBehaviourFactory weaponBehaviourFactory;

        private WeaponData activeWeaponData;
        private Dictionary<WeaponBehaviourData, IWeaponBehaviour> additionalComponents = new();
        
        private bool canShoot = true;
        private Coroutine shootDelayCoroutine;

        private bool hasAddedBehaviours;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ShootInputEvent>(TryShoot);
            signalBus.Subscribe<PowerUpCollectedEvent>(OnPowerUpCollected);
        }

        protected override void Start()
        {
            InitPoolData();
            UpdateWeaponSetup(defaultWeaponData);
        }

        private void Update()
        {
            if (!hasAddedBehaviours)
                return;
            
            if (additionalComponents == null || additionalComponents.Count == 0)
                return;
            
            foreach (var component in additionalComponents)
            {
                component.Value.Update();
            }
        }
        
        private void UpdateWeaponSetup(WeaponData weaponData)
        {
            if (activeWeaponData == weaponData)
                return;
            
            hasAddedBehaviours = false;
            
            activeWeaponData = weaponData;

            // Reset the weapon pooling
            Clear();
            Prefill();

            additionalComponents = new();
            foreach (var behaviourData in weaponData.BehaviourData)
            {
                var behaviourComponent = weaponBehaviourFactory.GetBoundComponent(
                    GetWeaponSpawnComponent(),
                    behaviourData);
                additionalComponents.Add(behaviourData, behaviourComponent);
            }

            hasAddedBehaviours = true;
        }
        
        private WeaponSpawnComponent GetWeaponSpawnComponent()
        {
            return new WeaponSpawnComponent
            {
                SpawnTransform = shipNozzle,
                PopCallback = projectileSpawnData =>
                {
                    var projectile = Pop(projectileSpawnData.Position, projectileSpawnData.Rotation);
                    projectile.SetProjectileData(activeWeaponData.ProjectileData, projectileSpawnData.Direction * activeWeaponData.Speed);
                }
            };
        }

        private void TryShoot()
        {
            if (!canShoot)
                return;

            if (activeWeaponData == null)
                return;
            
            var projectile = Pop(shipNozzle.position, shipNozzle.rotation);
            projectile.SetProjectileData(activeWeaponData.ProjectileData, shipNozzle.up * activeWeaponData.Speed);
            
            OnFire();
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
            }
            shootDelayCoroutine = StartCoroutine(SetShootDelayCoroutine());
        }
        
        private IEnumerator SetShootDelayCoroutine()
        {
            canShoot = false;
            yield return new WaitForSeconds(activeWeaponData.Delay);
            canShoot = true;
        }

        private void OnFire()
        {
            if (!hasAddedBehaviours)
                return;
            
            if (additionalComponents == null || additionalComponents.Count == 0)
                return;
            
            foreach (var component in additionalComponents)
            {
                component.Value.OnFire();
            }
        }

        private void OnPowerUpCollected(PowerUpCollectedEvent evt)
        {
            if (evt.PowerUp == null)
                return;
            
            UpdateWeaponSetup(evt.PowerUp);
        }

        protected override Projectile CreateObject()
        {
            return container.InstantiatePrefab(
                activeWeaponData.ProjectileData.ProjectilePrefab, 
                shipNozzle.position, 
                shipNozzle.rotation, 
                null
            ).GetComponent<Projectile>(); 
        }
        
        private void OnDisable()
        {
            signalBus.TryUnsubscribe<ShootInputEvent>(TryShoot);
            signalBus.TryUnsubscribe<PowerUpCollectedEvent>(OnPowerUpCollected);
            
            if (shootDelayCoroutine != null)
            {
                StopCoroutine(shootDelayCoroutine);
                shootDelayCoroutine = null;
            }

            Clear();
        }
    }
}