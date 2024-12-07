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
        
        public Vector2 GetPlayerPosition()
        {
            return player == null ? Vector2.zero : player.transform.position;
        }
        
        public void Dispose()
        {
            signalBus.TryUnsubscribe<PlayerNewSpawnEvent>(OnNewPlayerSpawned);
        }
    }
}