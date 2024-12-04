using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/HyperspaceData", fileName = "HyperspaceData")]
    public class PlayerHyperspaceData : ScriptableObject
    {
        [Tooltip("The duration of the hyperspace travel")]
        [SerializeField] private float duration;
        
        [Tooltip("The cooldown timer to be able to use hyperspace again")]
        [SerializeField] private float cooldown;

        public float Duration => duration;
        public float Cooldown => cooldown;
    }
}