using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Projectiles/Behaviours/DestroyOtherAfterCollision", fileName = "DestroyOtherAfterCollision")]
    public class ProjectileDestroyOtherAfterCollisionData : ProjectileBehaviourData
    {
        [Tooltip("Object tags to ignore when colliding")]
        [SerializeField] private string[] tagsToIgnore;
        
        public string[] TagsToIgnore => tagsToIgnore;
    }
}