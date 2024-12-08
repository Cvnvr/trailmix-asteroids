using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/UFOs/UfoData", fileName = "UfoData")]
    public class UfoData : EnemyData
    {
        [Header("General")]
        [Tooltip("The type of UFO")]
        [SerializeField] private UfoType ufoType;

        [Header("Movement")]
        [Tooltip("How frequently the UFO changes direction")]
        [SerializeField] private float changeDirectionInterval;

        [Tooltip("The tolerance for changing direction")]
        [SerializeField] private float changeDirectionTolerance;

        [Tooltip("The chance of dropping a power-up after being destroyed (between 0 and 1)")]
        [SerializeField] private float chanceOfDroppingPowerUp;

        [Header("Shooting")]
        [Tooltip("The time between shots")]
        [SerializeField] private float timeBetweenShots;
        
        [Tooltip("Whether or not the UFO should always fire towards the player")]
        [SerializeField] private bool shouldTargetPlayer;

        [Tooltip("How much the UFO has a chance to miss the player by")]
        [SerializeField] private float playerTargetDirectionTolerance;
        
        public UfoType UfoType => ufoType;
        
        public float ChangeDirectionInterval => changeDirectionInterval;
        public float ChangeDirectionTolerance => changeDirectionTolerance;
        public float ChanceOfDroppingPowerUp => chanceOfDroppingPowerUp;

        public float TimeBetweenShots => timeBetweenShots;
        public bool ShouldTargetPlayer => shouldTargetPlayer;
        public float PlayerTargetDirectionTolerance => playerTargetDirectionTolerance;
    }
}