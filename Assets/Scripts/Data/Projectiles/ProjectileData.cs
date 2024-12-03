using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/ProjectileData", fileName = "ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        [Tooltip("The prefab of the projectile.")]
        [SerializeField] private GameObject projectilePrefab;

        [Tooltip("Custom behaviours for the projectile.")]
        [SerializeField] private ProjectileBehaviourData[] behaviours;
        
        public GameObject ProjectilePrefab => projectilePrefab;
        public ProjectileBehaviourData[] Behaviours => behaviours;
    }
}