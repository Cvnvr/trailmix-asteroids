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

        private GameObject activePlayer;
        private Coroutine respawnCoroutine;
        
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
                if (respawnCoroutine != null)
                {
                    StopCoroutine(respawnCoroutine);
                }
                respawnCoroutine = StartCoroutine(SpawnAfterTimer());
            }
            else
            {
                SpawnPlayer();
            }
        }
        
        private void SpawnPlayer()
        {
            if (activePlayer != null)
            {
                Debug.LogWarning($"[{nameof(PlayerSpawner)}.{nameof(SpawnPlayer)}] Player already exists!");
                return;
            }
            
            Debug.Log($"[{nameof(PlayerSpawner)}.{nameof(SpawnPlayer)}] Spawning new player");

            activePlayer = container.InstantiatePrefab(playerData.Prefab, Vector3.zero, Quaternion.identity, null);
        }
        
        private IEnumerator SpawnAfterTimer()
        {
            yield return new WaitForSeconds(playerData.LifeData.RespawnDelay);
            
            SpawnPlayer();
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PlayerSpawnEvent>(OnPlayerSpawn);
            
            if (respawnCoroutine != null)
            {
                StopCoroutine(respawnCoroutine);
                respawnCoroutine = null;
            }
        }
    }
}