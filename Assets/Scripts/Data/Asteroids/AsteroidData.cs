using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Asteroids/AsteroidData", fileName = "AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [Header("Properties")]
        [Tooltip("The size of the asteroid")]
        [SerializeField] private AsteroidType asteroidType;

        [Tooltip("The prefab that gets spawned")]
        [SerializeField] private GameObject prefab;
        
        [Tooltip("Sprites which get chosen at random when the asteroid is created")]
        [SerializeField] private List<Sprite> sprites;
        
        [Tooltip("How fast the asteroid travels")]
        [SerializeField] private float movementSpeed;
        
        [Tooltip("The score the player gets for destroying this asteroid")]
        [SerializeField] private int score;
        
        [Header("Spawn Behaviour")]
        [Tooltip("Whether or not this asteroid spawns more asteroids when destroyed")]
        [SerializeField] private bool doesSpawnMoreOnDestruction;
        
        [Tooltip("How many this asteroid spawns when destroyed")]
        [SerializeField] private int numberToSpawn;
        
        [Tooltip("The data of the asteroids that get spawned")]
        [SerializeField] private AsteroidData spawnedAsteroidData;
        
        public AsteroidType AsteroidType => asteroidType;
        public GameObject Prefab => prefab;
        public List<Sprite> Sprites => sprites;
        public float MovementSpeed => movementSpeed;
        public int Score => score;
        
        public bool DoesSpawnMoreOnDestruction => doesSpawnMoreOnDestruction;
        public int NumberToSpawn => numberToSpawn;
        public AsteroidData SpawnedAsteroidData => spawnedAsteroidData;
    }
}