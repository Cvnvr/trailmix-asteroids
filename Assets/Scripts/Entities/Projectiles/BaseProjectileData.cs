using UnityEngine;

namespace Entities.Projectiles
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/Default", fileName = "New Projectile")]
    public class BaseProjectileData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;

        public GameObject Prefab => prefab;
        public float Speed => speed;
        public float Lifetime => lifetime;
    }
}