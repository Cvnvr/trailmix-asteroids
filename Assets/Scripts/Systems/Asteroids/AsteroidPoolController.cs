using System.Collections.Generic;
using Asteroids.Utils;
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
            {
                Debug.LogWarning($"[{nameof(UfoPoolController)}.{nameof(OnAsteroidSpawn)}] Attempted to spawn an Asteroid with invalid data");
                return;
            }
            
            if (pools == null || !pools.TryGetValue(evt.AsteroidData.AsteroidType, out var pool)) 
                return;
            
            for (var i = 0; i < evt.NumberToSpawn; i++)
            {
                var randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 361f));
                var asteroid = pool.Pop(evt.Position, randomRotation) as Asteroid;
                if (asteroid)
                {
                    asteroid.Setup(evt.AsteroidData, evt.Direction);
                }
            }
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedEvent evt)
        {
            if (evt.AsteroidData.DoesSpawnMoreOnDestruction)
            {
                for (var i = 0; i < evt.AsteroidData.NumberToSpawn; i++)
                {
                    OnAsteroidSpawn(new AsteroidSpawnEvent()
                    {
                        AsteroidData = evt.AsteroidData.SpawnedAsteroidData,
                        NumberToSpawn = 1,
                        Position = evt.Position,
                        Direction = (evt.Direction + VectorUtils.GetRandomVectorWithinTolerance(1f)).normalized
                    });
                }
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
            signalBus.TryUnsubscribe<AsteroidDestroyedEvent>(OnAsteroidDestroyed);
        }
    }
}