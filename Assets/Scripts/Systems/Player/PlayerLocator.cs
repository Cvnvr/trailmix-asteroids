using System;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class PlayerLocator : IDisposable
    {
        [Inject] private SignalBus signalBus;

        private GameObject player;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<PlayerNewSpawnEvent>(OnNewPlayerSpawned);
        }

        private void OnNewPlayerSpawned(PlayerNewSpawnEvent evt)
        {
            player = evt.Player;
        }
        
        public bool TryGetPlayerPosition(out Vector2 position)
        {
            if (player == null)
            {
                position = Vector2.zero;
                return false;
            }

            position = player.transform.position;
            return true;
        }
        
        public void Dispose()
        {
            signalBus.TryUnsubscribe<PlayerNewSpawnEvent>(OnNewPlayerSpawned);
        }
    }
}