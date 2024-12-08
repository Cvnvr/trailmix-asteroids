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

        private WeaponSpawnData spawnData;
        
        public void Setup(WeaponSpawnData spawnData, float angleOffset, float spawnDelay)
        {
            this.spawnData = spawnData;
            this.angleOffset = angleOffset;
            originalSpawnDelay = spawnDelay;

            Reset();
        }

        private void SpawnAdditionalProjectiles()
        {
            canSpawn = false;
            hasSpawned = true;
            
            // Fire the left projectile
            var leftRotation = Quaternion.AngleAxis(angleOffset, Vector3.forward) * spawnData.SpawnTransform.rotation;
            spawnData.PopCallback?.Invoke(spawnData.SpawnTransform.position, leftRotation, leftRotation * Vector2.up);

            // Fire the right projectile
            var rightRotation = Quaternion.AngleAxis(-angleOffset, Vector3.forward) * spawnData.SpawnTransform.rotation;
            spawnData.PopCallback?.Invoke(spawnData.SpawnTransform.position, rightRotation, rightRotation * Vector2.up);

            Reset();
        }
        
        public void OnFire()
        {
            // Begin counting down from the initial delay
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