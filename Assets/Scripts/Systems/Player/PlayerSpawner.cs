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

        private const string PlayerTag = "Player";

        private GameObject activePlayer;
        private Coroutine respawnCoroutine;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerTriggerSpawnEvent>(OnTriggeredSpawn);
        }

        public void Setup()
        {
            if (GameObject.FindWithTag(PlayerTag) != null)
            {
                Debug.LogWarning($"[{nameof(PlayerSpawner)}.{nameof(Setup)}] Player already exists in the scene, skipping spawn!");
                return;
            }
            
            SpawnPlayer();
        }

        private void OnTriggeredSpawn()
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

            activePlayer = container.InstantiatePrefab(
                playerData.Prefab, 
                Vector2.zero, 
                Quaternion.identity, 
                null
            );
            
            signalBus.TryFire(new PlayerNewSpawnEvent() { Player = activePlayer});
        }
        
        private IEnumerator SpawnAfterTimer()
        {
            yield return new WaitForSeconds(playerData.LifeData.RespawnDelay);
            
            SpawnPlayer();
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PlayerTriggerSpawnEvent>(OnTriggeredSpawn);
            
            if (respawnCoroutine != null)
            {
                StopCoroutine(respawnCoroutine);
                respawnCoroutine = null;
            }
        }
    }
}