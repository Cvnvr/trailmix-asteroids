using UnityEngine;

namespace Asteroids
{
    public class WeaponSpawnAdditionalComponent : IWeaponBehaviour
    {
        private float angleOffset;
        private float originalSpawnDelay;

        private bool canSpawn;
        private bool hasSpawned;
        private float delay;

        private WeaponSpawnComponent weaponSpawnComponent;
        
        public void Setup(WeaponSpawnComponent weaponSpawnComponent, float angleOffset, float spawnDelay)
        {
            this.weaponSpawnComponent = weaponSpawnComponent;
            this.angleOffset = angleOffset;
            originalSpawnDelay = spawnDelay;

            Reset();
        }

        private void SpawnAdditionalProjectiles()
        {
            canSpawn = false;
            hasSpawned = true;
            
            // Fire the left projectile
            var leftRotation = Quaternion.AngleAxis(angleOffset, Vector3.forward) * weaponSpawnComponent.SpawnTransform.rotation;
            weaponSpawnComponent.PopCallback?.Invoke(new ProjectileSpawnData()
            {
                Position = weaponSpawnComponent.SpawnTransform.position,
                Rotation = leftRotation, 
                Direction = leftRotation * Vector2.up
            });

            // Fire the right projectile
            var rightRotation = Quaternion.AngleAxis(-angleOffset, Vector3.forward) * weaponSpawnComponent.SpawnTransform.rotation;
            weaponSpawnComponent.PopCallback?.Invoke(new ProjectileSpawnData()
            {
                Position = weaponSpawnComponent.SpawnTransform.position,
                Rotation = rightRotation, 
                Direction = rightRotation * Vector2.up
            });

            Reset();
        }
        
        public void OnFire()
        {
            canSpawn = true;
        }

        public void Update()
        {
            if (!canSpawn || hasSpawned)
                return;
            
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                SpawnAdditionalProjectiles();
            }
        }

        public void Reset()
        {
            canSpawn = false;
            hasSpawned = false;
            delay = originalSpawnDelay;
        }
    }
}