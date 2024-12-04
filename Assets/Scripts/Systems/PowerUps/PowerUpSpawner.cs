using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField] private PowerUpItem prefab;
        [SerializeField] private PowerUpData[] powerUps;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PowerUpSpawnEvent>(OnPowerUpSpawned);
        }
        
        private void OnPowerUpSpawned(PowerUpSpawnEvent evt)
        {
            var powerUp = powerUps[Random.Range(0, powerUps.Length)];
            var powerUpItem = CreateItem(evt.SpawnPosition);
            powerUpItem.Init(powerUp);
        }
        
        private PowerUpItem CreateItem(Vector3 position)
        {
            return container.InstantiatePrefab(
                prefab, 
                position, 
                Quaternion.identity, 
                transform
            ).GetComponent<PowerUpItem>(); 
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PowerUpSpawnEvent>(OnPowerUpSpawned);
        }
    }
}