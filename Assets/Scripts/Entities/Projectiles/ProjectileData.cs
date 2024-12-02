using UnityEngine;

namespace Entities.Projectiles
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/Default", fileName = "New Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float speed;
        
        public GameObject Prefab => prefab;
        public float Speed => speed;
    }
}