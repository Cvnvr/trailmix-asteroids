using System.Collections.Generic;
using System.Linq;
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
        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;
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
                Debug.LogWarning($"[{nameof(AsteroidPoolController)}.{nameof(OnAsteroidSpawn)}] Attempted to spawn an Asteroid with invalid data");
                return;
            }

            for (var i = 0; i < evt.NumberToSpawn; i++)
            {
                var randomSpawnPosition = screenBoundsCalculator.GetRandomOffScreenPosition();
                var randomDirection = GetRandomDirection(randomSpawnPosition, evt.AsteroidData.SpawnDirectionTolerance);
                SpawnAsteroid(evt.AsteroidData, randomSpawnPosition, randomDirection);
            }
        }

        private void SpawnAsteroid(AsteroidData data, Vector2 position, Vector2 direction)
        {
            if (pools == null || !pools.TryGetValue(data.AsteroidType, out var pool))
            {
                Debug.LogWarning($"[{nameof(AsteroidPoolController)}.{nameof(OnAsteroidSpawn)}] Attempted to spawn an Asteroid with invalid pool");
                return;
            }
            
            var randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 361f));
            var asteroid = (Asteroid)pool.Pop(position, randomRotation);
            if (!asteroid)
            {
                Debug.LogWarning($"[{nameof(AsteroidPoolController)}.{nameof(OnAsteroidSpawn)}] Failed to spawn an Asteroid");
                return;
            }

            asteroid.Setup(data, direction);
        }
        
        private Vector2 GetRandomDirection(Vector2 position, float tolerance)
        {
            var direction = (screenBoundsCalculator.GetCenterOfScreen() - position).normalized;
            direction += VectorUtils.GetRandomVectorWithinTolerance(tolerance);
            return direction.normalized;
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedEvent evt)
        {
            // If this asteroid doesn't spawn anymore, check to see if it was the last one
            if (!evt.AsteroidData.DoesSpawnMoreOnDestruction)
            {
                ValidateIfEndOfWave();
                return;
            }

            for (var i = 0; i < evt.AsteroidData.NumberToSpawn; i++)
            {
                SpawnAsteroid(evt.AsteroidData.SpawnedAsteroidData, evt.Position, evt.Direction);
            }
        }

        private void ValidateIfEndOfWave()
        {
            // get total count for all pools pushed count 
            var totalPushedCount = pools.Values.Sum(pool => pool.PushedCount) - 1; // -1 for the asteroid that was just destroyed
            if (totalPushedCount > 0)
                return;
            
            signalBus.TryFire<SpawnNewWaveEvent>();
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
            signalBus.TryUnsubscribe<AsteroidDestroyedEvent>(OnAsteroidDestroyed);
        }
    }
}