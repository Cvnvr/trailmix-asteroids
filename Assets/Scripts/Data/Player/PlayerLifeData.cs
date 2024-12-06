using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/LifeData", order = 3, fileName = "LifeData")]
    public class PlayerLifeData : ScriptableObject
    {
        [Tooltip("The number of lives the player starts with")]
        [SerializeField] private uint numberOfLives;
       
        [Tooltip("The timed delay (in seconds) before the player respawns")]
        [SerializeField] private float respawnDelay;
        
        public uint NumberOfLives => numberOfLives;
        public float RespawnDelay => respawnDelay;
    }
}