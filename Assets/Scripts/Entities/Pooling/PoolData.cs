using UnityEngine;

namespace Entities.Pooling
{
    [CreateAssetMenu(menuName = "Data/Entities/Pooling/Data", fileName = "New Data")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int initialPoolSize;
        [SerializeField] private int maxPoolSize;
        
        public bool CollectionCheck => collectionCheck;
        public int InitialPoolSize => initialPoolSize;
        public int MaxPoolSize => maxPoolSize;
    }
}