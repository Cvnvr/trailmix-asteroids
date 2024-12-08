using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/HyperspaceData", order = 4, fileName = "HyperspaceData")]
    public class PlayerHyperspaceData : ScriptableObject
    {
        [Tooltip("The duration of the hyperspace travel (in seconds)")]
        [SerializeField] private float duration;
        
        [Tooltip("The cooldown timer (in seconds) to be able to use hyperspace again")]
        [SerializeField] private float cooldown;

        public float Duration => duration;
        public float Cooldown => cooldown;
    }
}