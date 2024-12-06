using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class PowerUpSpawner : BasePooler<PowerUpItem>
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
            var powerUpItem = Pop(evt.SpawnPosition, Quaternion.identity);
            powerUpItem.Init(powerUp);
        }
        
        protected override PowerUpItem CreateObject()
        {
            return container.InstantiatePrefab(
                prefab, 
                Vector2.zero, 
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