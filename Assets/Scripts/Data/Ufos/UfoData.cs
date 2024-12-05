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
        
        public UfoType UfoType => ufoType;
        public float TimeBetweenShots => timeBetweenShots;
        public float ChangeDirectionInterval => changeDirectionInterval;
    }
}