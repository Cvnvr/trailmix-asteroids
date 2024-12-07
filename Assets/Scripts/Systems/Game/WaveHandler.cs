using System.Collections;
using Asteroids.Utils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class WaveHandler : MonoBehaviour
    {
        [SerializeField] private LevelSetupData levelSetupData;
        
        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;
        [Inject] private SignalBus signalBus;

        private uint currentWave = 0;
        private Coroutine spawnWaveCoroutine;
        
        private uint spawnedUfoCount = 0;
        private float cachedTimeBetweenUfoSpawnChecks;

        private bool isInitialised;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<SpawnNewWaveEvent>(OnSpawnNewWave);
            signalBus.Subscribe<UfoDestroyedEvent>(UpdateUfoSpawnChecks);
            signalBus.Subscribe<UfoRemovedSelfEvent>(UpdateUfoSpawnChecks);
        }
        
        public void Setup()
        {
            SpawnWave(levelSetupData.AsteroidsInitialSpawnCount);
            currentWave++;

            cachedTimeBetweenUfoSpawnChecks = levelSetupData.UfoSpawnCheckTimeDelay;

            isInitialised = true;
        }

        private void Update()
        {
            if (!isInitialised)
                return;
            
            TryAndSpawnUfo();
        }

        private void OnSpawnNewWave()
        {
            var numberToSpawn = levelSetupData.AsteroidsInitialSpawnCount + (levelSetupData.AsteroidsAdditionalWaveSpawnCount * currentWave);
            
            uint cappedNumberToSpawn;
            
            // Treat 0 as no cap
            if (levelSetupData.AsteroidsMaxSpawnCount == 0)
            {
                cappedNumberToSpawn = numberToSpawn;
            }
            else
            {
                cappedNumberToSpawn = (uint)Mathf.Min(numberToSpawn, levelSetupData.AsteroidsMaxSpawnCount);
            }
            
            if (levelSetupData.TimeBetweenWaves > 0)
            {
                if (spawnWaveCoroutine != null)
                {
                    StopCoroutine(spawnWaveCoroutine);
                }
                spawnWaveCoroutine = StartCoroutine(SpawnWaveAfterDelay(cappedNumberToSpawn, levelSetupData.TimeBetweenWaves));
            }
            else
            {
                SpawnWave(cappedNumberToSpawn);
            }
            
            currentWave++;
        }
        
        private void SpawnWave(uint numberToSpawn)
        {
            for (var i = 0; i < numberToSpawn; i++)
            {
                var randomSpawnPosition = screenBoundsCalculator.GetRandomOffScreenPosition();
                signalBus.TryFire(new AsteroidSpawnEvent()
                {
                    AsteroidData = levelSetupData.AsteroidToSpawn,
                    NumberToSpawn = 1,
                    Position = randomSpawnPosition,
                    Direction = GetRandomDirection(randomSpawnPosition, levelSetupData.SpawnDirectionTolerance)
                });
            }
        }

        private IEnumerator SpawnWaveAfterDelay(uint numberToSpawn, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnWave(numberToSpawn);
        }

        private void TryAndSpawnUfo()
        {
            if (spawnedUfoCount >= levelSetupData.UfoMaxSpawnCount)
                return;
            
            cachedTimeBetweenUfoSpawnChecks -= Time.deltaTime;
            if (cachedTimeBetweenUfoSpawnChecks <= 0)
            {
                if (Random.value <= levelSetupData.UfoChanceToSpawn)
                {
                    SpawnUfo();
                }
                
                cachedTimeBetweenUfoSpawnChecks = levelSetupData.UfoSpawnCheckTimeDelay;
            }
        }

        private void SpawnUfo()
        {
            var randomSpawnPosition = screenBoundsCalculator.GetRandomOffScreenPosition();
            signalBus.TryFire(new UfoSpawnEvent()
            {
                Position = randomSpawnPosition,
                Direction = GetRandomDirection(randomSpawnPosition, levelSetupData.SpawnDirectionTolerance),
                SuccessCallback = (onSuccess) =>
                {
                    if (onSuccess)
                    {
                        spawnedUfoCount++;
                    }
                }
            });
        }
        
        private Vector2 GetRandomDirection(Vector2 position, float tolerance)
        {
            var direction = (screenBoundsCalculator.GetCenterOfScreen() - position).normalized;
            direction += VectorUtils.GetRandomVectorWithinTolerance(tolerance);
            return direction.normalized;
        }
        
        private void UpdateUfoSpawnChecks()
        {
            if (spawnedUfoCount == 0)
                return;

            spawnedUfoCount--;
        }
        
        private void OnDisable()
        {
            signalBus.TryUnsubscribe<SpawnNewWaveEvent>(OnSpawnNewWave);
            signalBus.TryUnsubscribe<UfoDestroyedEvent>(UpdateUfoSpawnChecks);
            signalBus.TryUnsubscribe<UfoRemovedSelfEvent>(UpdateUfoSpawnChecks);
            
            if (spawnWaveCoroutine != null)
            {
                StopCoroutine(spawnWaveCoroutine);
                spawnWaveCoroutine = null;
            }
        }
    }
}