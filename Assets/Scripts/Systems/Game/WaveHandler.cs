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

        private int currentWave = 0;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<SpawnNewWaveEvent>(OnSpawnNewWave);
        }
        
        private void Start()
        {
            SpawnWave(levelSetupData.InitialNumberToSpawn);
            currentWave++;
        }

        private void OnSpawnNewWave()
        {
            var numberToSpawn = levelSetupData.InitialNumberToSpawn + (levelSetupData.AdditionalNumberToSpawnEachWave * currentWave);
            var cappedNumberToSpawn = Mathf.Min(numberToSpawn, levelSetupData.MaxNumberToSpawn);
            SpawnWave(cappedNumberToSpawn);
            currentWave++;
        }

        private void SpawnWave(int numberToSpawn)
        {
            for (var i = 0; i < numberToSpawn; i++)
            {
                signalBus.Fire(new AsteroidSpawnEvent()
                {
                    AsteroidData = levelSetupData.AsteroidToSpawn,
                    NumberToSpawn = 1,
                    Position = GetRandomOffScreenPosition()
                });
            }
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
        }
    }
}