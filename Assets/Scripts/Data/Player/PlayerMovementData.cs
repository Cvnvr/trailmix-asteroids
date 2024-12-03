using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/Movement Data", order = 1, fileName = "New Movement Data")]
    public class PlayerMovementData : ScriptableObject
    {
        [SerializeField] private float forwardThrust;
        [SerializeField] private float maxForwardSpeed;
        [SerializeField] private float rotationalThrust;
        [SerializeField] private float maxRotationalSpeed;
        
        public float ForwardThrust => forwardThrust;
        public float MaxForwardSpeed => maxForwardSpeed;
        public float RotationalThrust => rotationalThrust;
        public float MaxRotationalSpeed => maxRotationalSpeed;
    }
}