using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Weapons/Behaviours/SpawnAdditional", fileName = "SpawnAdditional")]
    public class WeaponSpawnAdditionalData : WeaponBehaviourData
    {
        [Tooltip("The angle offset for the additional projectiles.")]
        [SerializeField] private float angleOffset;

        [Tooltip("The delay between the additional projectiles being spawned.")]
        [SerializeField] private float spawnDelay;
        
        public float AngleOffset => angleOffset;
        public float SpawnDelay => spawnDelay;
    }
}