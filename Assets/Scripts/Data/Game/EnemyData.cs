using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Enemy/EnemyData", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Tooltip("The prefab that gets spawned")]
        [SerializeField] private GameObject prefab;
        
        [Tooltip("How fast the enemy travels")]
        [SerializeField] private float movementSpeed;
        
        [Tooltip("How much score gets rewarded for destroying this enemy")]
        [SerializeField] private uint score;
        
        public GameObject Prefab => prefab;
        public float MovementSpeed => movementSpeed;
        public uint Score => score;
    }
}