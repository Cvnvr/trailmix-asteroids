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

            var leftRotation = Quaternion.Euler(0, 0, spawnData.SpawnTransform.rotation.eulerAngles.z - angleOffset);
            var rightRotation = Quaternion.Euler(0, 0, spawnData.SpawnTransform.rotation.eulerAngles.z + angleOffset);

            spawnData.PopCallback?.Invoke(spawnData.SpawnTransform.position, leftRotation);
            spawnData.PopCallback?.Invoke(spawnData.SpawnTransform.position, rightRotation);

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