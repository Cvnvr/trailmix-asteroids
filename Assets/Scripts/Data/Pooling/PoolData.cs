using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Pooling/Data", fileName = "New Data")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private int initialPoolSize;
        [SerializeField] private int maxPoolSize;
        
        public int InitialPoolSize => initialPoolSize;
        public int MaxPoolSize => maxPoolSize;
    }
}