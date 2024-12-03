using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public struct WeaponSpawnData
    {
        public Transform SpawnTransform;
        public Action<Vector3, Quaternion> PopCallback;
    }
    
    public class PlayerShooterController : BasePooler<Projectile>
    {
        [SerializeField] private WeaponData weaponData;

        [SerializeField] private Transform shipNozzle;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        [Inject] private IWeaponBehaviourFactory weaponBehaviourFactory;

        private Dictionary<WeaponBehaviourData, IWeaponBehaviour> additionalComponents = new();
        
        private bool canShoot = true;
        private Coroutine shootDelayCoroutine;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ShootInputEvent>(TryShoot);
        }

        protected override void Start()
        {
            base.Start();

            UpdateWeaponSetup(weaponData);
        }

        private void Update()
        {
            foreach (var component in additionalComponents)
            {
                component.Value.Update();
            }
        }
        
        // TODO convert to event
        public void UpdateWeaponSetup(WeaponData weaponData)
        {
            this.weaponData = weaponData;

            additionalComponents = new();
            foreach (var behaviourData in weaponData.BehaviourData)
            {
                var behaviourComponent = weaponBehaviourFactory.GetBoundComponent(
                    GetWeaponSpawnData(),
                    behaviourData);
                additionalComponents.Add(behaviourData, behaviourComponent);
            }
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
            yield return new WaitForSeconds(weaponData.Delay);
            canShoot = true;
        }

        private void OnFire()
        {
            foreach (var component in additionalComponents)
            {
                component.Value.OnFire();
            }
        }

        protected override Projectile CreateObject()
        {
            return container.InstantiatePrefab(
                weaponData.ProjectileData.ProjectilePrefab, 
                shipNozzle.position, 
                shipNozzle.rotation, 
                transform
            ).GetComponent<Projectile>();
        }

        protected override void ActivateObject(Projectile item)
        {
            base.ActivateObject(item);
            
            item.SetProjectileData(weaponData.ProjectileData);
            item.Fire(shipNozzle.up * weaponData.Speed);
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