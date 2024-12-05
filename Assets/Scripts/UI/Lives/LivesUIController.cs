using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class LivesUIController : BasePooler<LifeUIElement>
    {
        [SerializeField] private GameObject lifeUIPrefab;
        
        [Inject] private SignalBus signalBus;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerLivesCountUpdatedEvent>(OnPlayerLivesCountUpdated);
        }
        
        private void OnPlayerLivesCountUpdated(PlayerLivesCountUpdatedEvent evt)
        {
            var currentUIElementsCount = PushedCount;

            if (evt.NewLivesCount == currentUIElementsCount)
                return;
            
            if (evt.NewLivesCount > currentUIElementsCount)
            {
                for (var i = 0; i < evt.NewLivesCount - currentUIElementsCount; i++)
                {
                    Pop();
                }
            }
            else if (evt.NewLivesCount < currentUIElementsCount)
            {
                for (var i = 0; i < currentUIElementsCount - evt.NewLivesCount; i++)
                {
                    Push();
                }
            }
        }
        
        protected override LifeUIElement CreateObject()
        {
            return Instantiate(lifeUIPrefab, transform).GetComponent<LifeUIElement>();
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<PlayerLivesCountUpdatedEvent>(OnPlayerLivesCountUpdated);
        }
    }
}