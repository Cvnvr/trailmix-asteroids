using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/PowerUps/PowerUpData", fileName = "PowerUpData")]
    public class PowerUpData : ScriptableObject
    {
        [Tooltip("The name of the power up")]
        [SerializeField] private string powerUpName;

        [Tooltip("The weapon data to be used")]
        [SerializeField] private WeaponData weaponData;
        
        [Tooltip("The sprite to set on the power up object")]
        [SerializeField] private Sprite sprite;

        [Tooltip("Whether the power up is timed or not")]
        [SerializeField] private bool timerEnabled;
        
        [Tooltip("Timer for the power up to remain on screen")]
        [SerializeField] private float timer;

        public string PowerUpName => powerUpName;
        public WeaponData WeaponData => weaponData;
        public Sprite Sprite => sprite;
        public bool TimerEnabled => timerEnabled;
        public float Timer => timer;
    }
}
