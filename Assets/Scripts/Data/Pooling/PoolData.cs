using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Pooling/Data", fileName = "New Data")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private uint initialPoolSize;
        
        public uint InitialPoolSize => initialPoolSize;
    }
}