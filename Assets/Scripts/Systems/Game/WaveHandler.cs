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

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<SpawnNewWaveEvent>(OnSpawnNewWave);
        }
        
        public void Setup()
        {
            SpawnWave(levelSetupData.InitialNumberToSpawn);
            currentWave++;
        }

        private void OnSpawnNewWave()
        {
            var numberToSpawn = levelSetupData.InitialNumberToSpawn + (levelSetupData.AdditionalNumberToSpawnEachWave * currentWave);
            
            uint cappedNumberToSpawn;
            
            // Treat 0 as no cap
            if (levelSetupData.MaxNumberToSpawn == 0)
            {
                cappedNumberToSpawn = numberToSpawn;
            }
            else
            {
                cappedNumberToSpawn = (uint)Mathf.Min(numberToSpawn, levelSetupData.MaxNumberToSpawn);
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
            
            SpawnWave(levelSetupData.InitialNumberToSpawn);
        }

        private Vector3 GetRandomOffScreenPosition()
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

            return new Vector3(xPos, yPos, 0);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<SpawnNewWaveEvent>(OnSpawnNewWave);
            
            if (spawnWaveCoroutine != null)
            {
                StopCoroutine(spawnWaveCoroutine);
                spawnWaveCoroutine = null;
            }
        }
    }
}