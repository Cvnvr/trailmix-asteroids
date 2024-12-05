using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    // TODO change this
    public struct WeaponSpawnData
    {
        public Transform SpawnTransform;
        public Action<Vector3, Quaternion> PopCallback;
    }
    
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

        private bool areBehavioursSet;
        
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
            if (!areBehavioursSet)
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
            areBehavioursSet = false;
            
            activeWeaponData = weaponData;

            // Reset the weapon pooling
            Clear();
            Prefill();

            additionalComponents = new();
            foreach (var behaviourData in weaponData.BehaviourData)
            {
                var behaviourComponent = weaponBehaviourFactory.GetBoundComponent(
                    GetWeaponSpawnData(),
                    behaviourData);
                additionalComponents.Add(behaviourData, behaviourComponent);
            }

            areBehavioursSet = true;
        }

        private WeaponSpawnData GetWeaponSpawnData()
        {
            return new WeaponSpawnData
            {
                SpawnTransform = shipNozzle,
                PopCallback = (position, rotation) => Pop(position, rotation)
            };
        }

        private void TryShoot()
        {
            if (!canShoot)
                return;

            if (activeWeaponData == null)
                return;
            
            Pop(shipNozzle.position, shipNozzle.rotation);
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
            if (!areBehavioursSet)
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

        protected override void ActivateObject(Projectile item)
        {
            base.ActivateObject(item);
            
            item.SetProjectileData(activeWeaponData.ProjectileData);
            item.Move(shipNozzle.up * activeWeaponData.Speed);
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