using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Asteroids/AsteroidData", fileName = "AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [Header("Properties")]
        [SerializeField] private AsteroidType asteroidType;
        [SerializeField] private GameObject prefab;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private float movementSpeed;

        [Header("Spawn Behaviour")]
        [SerializeField] private bool doesSpawnMoreOnDestruction;
        [SerializeField] private int numberToSpawn;
        [SerializeField] private AsteroidData spawnedAsteroidData;
        
        public AsteroidType AsteroidType => asteroidType;
        public GameObject Prefab => prefab;
        public List<Sprite> Sprites => sprites;
        public float MovementSpeed => movementSpeed;
        
        public bool DoesSpawnMoreOnDestruction => doesSpawnMoreOnDestruction;
        public int NumberToSpawn => numberToSpawn;
        public AsteroidData SpawnedAsteroidData => spawnedAsteroidData;
    }
}