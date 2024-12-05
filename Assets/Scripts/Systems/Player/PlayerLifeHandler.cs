using System.Collections;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class PlayerLifeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerLifeData lifeData;
        [SerializeField] private GameObject playerPrefab;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;

        private int currentLivesCount;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }

        private void Start()
        {
            currentLivesCount = lifeData.NumberOfLives;
            
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            container.InstantiatePrefab(playerPrefab, Vector3.zero, Quaternion.identity, null);
        }

        private void OnPlayerDestroyed()
        {
            currentLivesCount = Mathf.Max(currentLivesCount--, 0);
            
            Debug.Log($"[{nameof(PlayerLifeHandler)}.{nameof(OnPlayerDestroyed)}] Player destroyed. Remaining lives: {currentLivesCount}");

            signalBus.Fire(new PlayerLivesCountUpdatedEvent()
            {
                PreviousLivesCount = currentLivesCount + 1,
                NewLivesCount = currentLivesCount
            });

            if (currentLivesCount > 0)
            {
                StartCoroutine(RespawnAfterTimer());
            }
            else
            {
                signalBus.Fire<GameOverEvent>();
            }
        }

        private IEnumerator RespawnAfterTimer()
        {
            yield return new WaitForSeconds(lifeData.RespawnDelay);
            
            SpawnPlayer();
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }
    }
}