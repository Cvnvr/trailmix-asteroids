using UnityEngine;

namespace Entities.Projectiles
{
    public class ProjectileSpawnAdditionalComponent : BaseProjectileBehaviourComponent
    {
        private ProjectileData spawnedProjectileData;
        private int numberToSpawn;
        private Vector3 spawnOffset;
        private float spawnDelay;

        private bool canSpawn;
        private bool hasSpawned;
        
        public void Setup(Projectile projectile, ProjectileData spawnedProjectileData, 
            int numberToSpawn, Vector3 spawnOffset, float spawnDelay)
        {
            Init(projectile);
            
            this.spawnedProjectileData = spawnedProjectileData;
            this.numberToSpawn = numberToSpawn;
            this.spawnOffset = spawnOffset;
            this.spawnDelay = spawnDelay;

            canSpawn = true;
            hasSpawned = false;

            if (numberToSpawn == 0)
            {
                Debug.LogWarning($"[{nameof(ProjectileSpawnAdditionalComponent)}] numberToSpawn set to zero, so nothing will happen! Disabling component.");
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

        public override void Update()
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

        public override void OnCollision(GameObject other)
        {
        }
    }
}