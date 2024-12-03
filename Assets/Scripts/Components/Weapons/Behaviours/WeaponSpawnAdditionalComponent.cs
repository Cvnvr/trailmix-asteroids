using UnityEngine;

namespace Asteroids
{
    public class WeaponSpawnAdditionalComponent : IWeaponBehaviour
    {
        private ProjectileData spawnedProjectileData;
        private int numberToSpawn;
        private Vector3 spawnOffset;
        private float spawnDelay;

        private bool canSpawn;
        private bool hasSpawned;
        
        public void Init()
        {
        }
        
        public void Setup(ProjectileData spawnedProjectileData, 
            int numberToSpawn, Vector3 spawnOffset, float spawnDelay)
        {
            this.spawnedProjectileData = spawnedProjectileData;
            this.numberToSpawn = numberToSpawn;
            this.spawnOffset = spawnOffset;
            this.spawnDelay = spawnDelay;

            canSpawn = true;
            hasSpawned = false;

            if (numberToSpawn == 0)
            {
                Debug.LogWarning($"[{nameof(WeaponSpawnAdditionalComponent)}] numberToSpawn set to zero, so nothing will happen! Disabling component.");
                canSpawn = false;
            }
        }

        private void SpawnAdditionalProjectiles()
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                // TODO do something
            }
        }

        public void Update()
        {
            if (!canSpawn || hasSpawned)
                return;
            
            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0)
            {
                SpawnAdditionalProjectiles();
                hasSpawned = true;
            }
        }
    }
}