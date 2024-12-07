using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Game/LevelSetupData", fileName = "LevelSetupData")]
    public class LevelSetupData : ScriptableObject
    {
        [Tooltip("The type of Asteroid that spawns each wave")]
        [SerializeField] private AsteroidData asteroidToSpawn;
        
        [Tooltip("How many of those Asteroids to spawn")]
        [SerializeField] private uint asteroidsInitialSpawnCount;
        
        [Tooltip("How many additional to spawn each new wave")]
        [SerializeField] private uint asteroidsAdditionalWaveSpawnCount;

        [Tooltip("The max number that can be spawned at any point")]
        [SerializeField] private uint asteroidsMaxSpawnCount;
        
        [Tooltip("Timed delay (in seconds) between waves")]
        [SerializeField] private float timeBetweenWaves;
        
        [Tooltip("The odds that a UFO will spawn")]
        [SerializeField] private float ufoChanceToSpawn;

        [Tooltip("The max number of UFOs that can be spawned at any point")]
        [SerializeField] private uint ufoMaxSpawnCount;

        [Tooltip("Timed delay (in seconds) between UFO spawn checks")]
        [SerializeField] private float ufoSpawnCheckTimeDelay;
        
        public AsteroidData AsteroidToSpawn => asteroidToSpawn;
        public uint AsteroidsInitialSpawnCount => asteroidsInitialSpawnCount;
        public uint AsteroidsAdditionalWaveSpawnCount => asteroidsAdditionalWaveSpawnCount;
        public uint AsteroidsMaxSpawnCount => asteroidsMaxSpawnCount;
        public float TimeBetweenWaves => timeBetweenWaves;
        public float UfoChanceToSpawn => ufoChanceToSpawn;
        public uint UfoMaxSpawnCount => ufoMaxSpawnCount;
        public float UfoSpawnCheckTimeDelay => ufoSpawnCheckTimeDelay;
    }
}