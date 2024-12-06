using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class PlayerLifeHandler : MonoBehaviour
    {
        [SerializeField] private PlayerLifeData lifeData;

        [Inject] private SignalBus signalBus;

        private uint currentLivesCount;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }

        private void Start()
        {
            currentLivesCount = lifeData.NumberOfLives;
            
            // Init the lives UI
            signalBus.TryFire(new PlayerLivesCountUpdatedEvent()
            {
                PreviousLivesCount = currentLivesCount,
                NewLivesCount = currentLivesCount
            });
        }

        private void OnPlayerDestroyed()
        {
            if (currentLivesCount <= 0)
                return;

            var previousLivesCount = currentLivesCount;
            currentLivesCount--;
            
            Debug.Log($"[{nameof(PlayerLifeHandler)}.{nameof(OnPlayerDestroyed)}] Player destroyed. Remaining lives: {currentLivesCount}");

            signalBus.TryFire(new PlayerLivesCountUpdatedEvent()
            {
                PreviousLivesCount = previousLivesCount,
                NewLivesCount = currentLivesCount
            });

            if (currentLivesCount > 0)
            {
                signalBus.TryFire<PlayerSpawnEvent>();
            }
            else
            {
                signalBus.TryFire<GameOverEvent>();
            }
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PlayerDestroyedEvent>(OnPlayerDestroyed);
        }
    }
}