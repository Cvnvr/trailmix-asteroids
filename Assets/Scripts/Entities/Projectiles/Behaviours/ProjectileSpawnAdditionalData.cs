using UnityEngine;

namespace Entities.Projectiles
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/Behaviours/SpawnAdditional", fileName = "SpawnAdditional")]
    public class ProjectileSpawnAdditionalData : ProjectileBehaviourData
    {
        [Tooltip("Settings for the additional spawned projectiles.")]
        [SerializeField] private ProjectileData spawnedProjectileData;

        [Tooltip("The number of additional projectiles to spawn.")]
        [SerializeField] private int numberToSpawn;
        
        [Tooltip("Offset for the spawn position.")]
        [SerializeField] private Vector3 spawnOffset;

        [Tooltip("The delay between the additional projectiles being spawned.")]
        [SerializeField] private float spawnDelay;
        
        public ProjectileData SpawnedProjectileData => spawnedProjectileData;
        public int NumberToSpawn => numberToSpawn;
        public Vector3 SpawnOffset => spawnOffset;
        public float SpawnDelay => spawnDelay;
    }
}