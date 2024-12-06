using UnityEngine;

namespace Asteroids
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private WaveHandler waveHandler;
        
        private void Start()
        {
            if (playerSpawner)
            {
                playerSpawner.Setup();
            }

            if (waveHandler)
            {
                waveHandler.Setup();
            }
        }
    }
}