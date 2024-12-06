using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Pooling/Data", fileName = "New Data")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private uint initialPoolSize;
        [SerializeField] private uint maxPoolSize;
        
        public uint InitialPoolSize => initialPoolSize;
        public uint MaxPoolSize => maxPoolSize;
    }
}