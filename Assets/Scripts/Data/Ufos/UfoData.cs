using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/UFOs/UfoData", fileName = "UfoData")]
    public class UfoData : EnemyData
    {
        [Tooltip("The type of UFO")]
        [SerializeField] private UfoType ufoType;
        
        [Tooltip("The time between shots")]
        [SerializeField] private float timeBetweenShots;

        [Tooltip("How frequently the UFO changes direction")]
        [SerializeField] private float changeDirectionInterval;

        [Tooltip("The tolerance for changing direction")]
        [SerializeField] private float changeDirectionTolerance;

        [Tooltip("The chance of dropping a power-up after being destroyed (between 0 and 1)")]
        [SerializeField] private float chanceOfDroppingPowerUp;
        
        public UfoType UfoType => ufoType;
        public float TimeBetweenShots => timeBetweenShots;
        public float ChangeDirectionInterval => changeDirectionInterval;
        public float ChangeDirectionTolerance => changeDirectionTolerance;
        public float ChanceOfDroppingPowerUp => chanceOfDroppingPowerUp;
    }
}