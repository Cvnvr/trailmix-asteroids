using System.Collections;
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

            isInitialised = true;
        }

        private void Update()
        {
            if (!isInitialised)
                return;

            if (spawnedUfoCount >= levelSetupData.UfoMaxSpawnCount)
                return;
            
            cachedTimeBetweenUfoSpawnChecks -= Time.deltaTime;
            if (cachedTimeBetweenUfoSpawnChecks <= 0)
            {
                TryAndSpawnUfo();
            }
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
                spawnWaveCoroutine = StartCoroutine(SpawnWaveAfterDelay(levelSetupData.TimeBetweenWaves));
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
                signalBus.TryFire(new AsteroidSpawnEvent()
                {
                    AsteroidData = levelSetupData.AsteroidToSpawn,
                    NumberToSpawn = 1,
                    Position = GetRandomOffScreenPosition()
                });
            }
        }

        private IEnumerator SpawnWaveAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnWave(levelSetupData.AsteroidsInitialSpawnCount);
        }

        private Vector2 GetRandomOffScreenPosition()
        {
            var xPos = 0f;
            var yPos = 0f;
            
            var edge = Random.Range(0, 4);
            switch (edge)
            {
                case 0: // Top edge
                    xPos = Random.Range(screenBoundsCalculator.LeftSide, screenBoundsCalculator.RightSide);
                    yPos = screenBoundsCalculator.TopSide;
                    break;
                case 1: // Bottom edge
                    xPos = Random.Range(screenBoundsCalculator.LeftSide, screenBoundsCalculator.RightSide);
                    yPos = screenBoundsCalculator.BottomSide;
                    break;
                case 2: // Left edge
                    xPos = screenBoundsCalculator.LeftSide;
                    yPos = Random.Range(screenBoundsCalculator.BottomSide, screenBoundsCalculator.TopSide);
                    break;
                case 3: // Right edge
                    xPos = screenBoundsCalculator.RightSide;
                    yPos = Random.Range(screenBoundsCalculator.BottomSide, screenBoundsCalculator.TopSide);
                    break;
                default:
                    xPos = 0f;
                    yPos = 0f;
                    break;
            }

            return new Vector2(xPos, yPos);
        }

        private void TryAndSpawnUfo()
        {
            if (Random.value > levelSetupData.UfoChanceToSpawn)
                return;
            
            signalBus.TryFire(new UfoSpawnEvent()
            {
                Position = GetRandomOffScreenPosition(),
                SuccessCallback = (onSuccess) =>
                {
                    if (onSuccess)
                    {
                        spawnedUfoCount++;
                    }
                }
            });
            
            cachedTimeBetweenUfoSpawnChecks = levelSetupData.UfoSpawnCheckTimeDelay;
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
            signalBus.TryUnsubscribe<UfoDestroyedEvent>(OnSpawnNewWave);
            signalBus.TryUnsubscribe<UfoRemovedSelfEvent>(OnSpawnNewWave);
            
            if (spawnWaveCoroutine != null)
            {
                StopCoroutine(spawnWaveCoroutine);
                spawnWaveCoroutine = null;
            }
        }
    }
}