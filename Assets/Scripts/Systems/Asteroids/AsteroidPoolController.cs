using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPoolController : MonoBehaviour
    {
        [SerializeField] private PoolData poolData;
        [SerializeField] private AsteroidData[] asteroidData;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        
        private Dictionary<AsteroidType, EnemyPooler> pools;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
            signalBus.Subscribe<AsteroidDestroyedEvent>(OnAsteroidDestroyed);
        }
        
        private void Awake()
        {
            pools = new();
            foreach (var data in asteroidData)
            {
                var pool = container.InstantiateComponent<EnemyPooler>(gameObject);
                pool.Init(data, poolData);
                pools.Add(data.AsteroidType, pool);
            }
        }

        private void OnAsteroidSpawn(AsteroidSpawnEvent evt)
        {
            if (evt.AsteroidData == null)
                return;
            
            if (!pools.TryGetValue(evt.AsteroidData.AsteroidType, out var pool)) 
                return;
            
            for (var i = 0; i < evt.NumberToSpawn; i++)
            {
                var asteroid = pool.Pop(evt.Position, Quaternion.Euler(0, 0, Random.Range(0f, 361f))).GetComponent<Asteroid>();
                asteroid.Setup(evt.AsteroidData);
                asteroid.Move(asteroid.transform.up);
            }
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedEvent evt)
        {
            if (evt.AsteroidData.DoesSpawnMoreOnDestruction)
            {
                OnAsteroidSpawn(new AsteroidSpawnEvent()
                {
                    AsteroidData = evt.AsteroidData.SpawnedAsteroidData,
                    NumberToSpawn = evt.AsteroidData.NumberToSpawn,
                    Position = evt.Position
                });
                return;
            }

            ValidateIfEndOfWave();
        }

        private void ValidateIfEndOfWave()
        {
            var isEmpty = true;
            foreach (var pool in pools)
            {
                if (pool.Value.PushedCount > 0)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                signalBus.TryFire<SpawnNewWaveEvent>();
            }
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
        }
    }
}