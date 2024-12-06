using UnityEngine;

namespace Asteroids
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private WaveHandler waveHandler;
        
        private void Start()
        {
            if (playerSpawner == null)
            {
                Debug.LogError($"[{nameof(Game)}.{nameof(Start)}] No {nameof(PlayerSpawner)} reference found.");
                return;
            }

            if (waveHandler == null)
            {
                Debug.LogError($"[{nameof(Game)}.{nameof(Start)}] No {nameof(WaveHandler)} reference found.");
                return;
            }
            
            playerSpawner.Setup();
            waveHandler.Setup();
        }
    }
}