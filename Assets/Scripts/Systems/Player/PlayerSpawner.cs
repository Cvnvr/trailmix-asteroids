using System.Collections;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerSpawnEvent>(OnPlayerSpawn);
        }

        public void Setup()
        {
            SpawnPlayer();
        }

        private void OnPlayerSpawn()
        {
            if (playerData.LifeData.RespawnDelay > 0)
            {
                StartCoroutine(SpawnAfterTimer());
            }
            else
            {
                SpawnPlayer();
            }
        }
        
        private void SpawnPlayer()
        {
            container.InstantiatePrefab(playerData.Prefab, Vector3.zero, Quaternion.identity, null);
        }
        
        private IEnumerator SpawnAfterTimer()
        {
            yield return new WaitForSeconds(playerData.LifeData.RespawnDelay);
            
            SpawnPlayer();
        }
    }
}