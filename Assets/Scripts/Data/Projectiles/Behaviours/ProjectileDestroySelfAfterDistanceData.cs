using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Projectiles/Behaviours/DestroySelfAfterDistance", fileName = "DestroySelfAfterDistance")]
    public class ProjectileDestroySelfAfterDistanceData : ProjectileBehaviourData
    {
        [Tooltip("The amount of distance the projectile can travel before it's destroyed.")]
        [SerializeField] private float distance;
        
        public float Distance => distance;
    }
}