using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Asteroids/AsteroidData", fileName = "AsteroidData")]
    public class AsteroidData : EnemyData
    {
        [Tooltip("The size of the asteroid")]
        [SerializeField] private AsteroidType asteroidType;

        [Tooltip("Sprites which get chosen at random when the asteroid is created")]
        [SerializeField] private Sprite[] sprites;
        
        [Tooltip("Whether or not this asteroid spawns more asteroids when destroyed")]
        [SerializeField] private bool doesSpawnMoreOnDestruction;
        
        [Tooltip("How many this asteroid spawns when destroyed")]
        [SerializeField] private uint numberToSpawn;
        
        [Tooltip("The data of the asteroids that get spawned")]
        [SerializeField] private AsteroidData spawnedAsteroidData;
        
        public AsteroidType AsteroidType => asteroidType;
        public Sprite[] Sprites => sprites;
        
        public bool DoesSpawnMoreOnDestruction => doesSpawnMoreOnDestruction;
        public uint NumberToSpawn => numberToSpawn;
        public AsteroidData SpawnedAsteroidData => spawnedAsteroidData;
    }
}