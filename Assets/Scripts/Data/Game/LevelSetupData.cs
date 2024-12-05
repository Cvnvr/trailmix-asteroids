using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Game/LevelSetupData", fileName = "LevelSetupData")]
    public class LevelSetupData : ScriptableObject
    {
        [Tooltip("The type of Asteroid that spawns each wave")]
        [SerializeField] private AsteroidData asteroidToSpawn;
        
        [Tooltip("How many of those Asteroids to spawn")]
        [SerializeField] private int initialNumberToSpawn;
        
        [Tooltip("How many additional to spawn each new wave")]
        [SerializeField] private int additionalNumberToSpawnEachWave;

        [Tooltip("The max number that can be spawned at any point")]
        [SerializeField] private int maxNumberToSpawn;
        
        [Tooltip("Timed delay between waves (seconds)")]
        [SerializeField] private float timeBetweenWaves;
        
        [Tooltip("The odds that a UFO will spawn ")]
        [SerializeField] private float chanceToSpawnUfo;
        
        public AsteroidData AsteroidToSpawn => asteroidToSpawn;
        public int InitialNumberToSpawn => initialNumberToSpawn;
        public int AdditionalNumberToSpawnEachWave => additionalNumberToSpawnEachWave;
        public int MaxNumberToSpawn => maxNumberToSpawn;
        public float TimeBetweenWaves => timeBetweenWaves;
        public float ChanceToSpawnUfo => chanceToSpawnUfo;
    }
}