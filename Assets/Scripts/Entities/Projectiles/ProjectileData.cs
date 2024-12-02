using UnityEngine;

namespace Entities.Projectiles
{
    [CreateAssetMenu(menuName = "Data/Entities/Projectiles/Default", fileName = "New Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float delay;

        public float Speed => speed;
        public float Delay => delay;
    }
}