using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Projectiles/Behaviours/DestroySelfAfterTime", fileName = "DestroySelfAfterTime")]
    public class ProjectileDestroySelfAfterTimeData : ProjectileBehaviourData
    {
        [Tooltip("The amount of time that needs to expire before the projectile is destroyed.")]
        [SerializeField] private float lifetime;
        
        public float Lifetime => lifetime;
    }
}