using UnityEngine;

namespace Entities.Projectiles
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/ProjectileData", fileName = "ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        [Tooltip("The prefab of the projectile.")]
        [SerializeField] private GameObject projectilePrefab;

        [Tooltip("The speed that the projectile accelerates at.")]
        [SerializeField] private float speed;

        [Tooltip("Delay before another projectile can be fired.")]
        [SerializeField] private float delay;

        [Tooltip("List of behaviors to apply to the projectile.")]
        [SerializeField] private ProjectileBehaviourData[] behaviours;
        
        public GameObject ProjectilePrefab => projectilePrefab;
        public float Speed => speed;
        public float Delay => delay;
        public ProjectileBehaviourData[] Behaviours => behaviours;
    }
}