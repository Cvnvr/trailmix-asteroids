using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Weapons/WeaponData", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Tooltip("The data for the projectile that this weapon fires.")]
        [SerializeField] private ProjectileData projectileData;

        [Tooltip("The speed of the projectile that gets fired.")]
        [SerializeField] private float speed;
        
        [Tooltip("Delay before another projectile can be fired.")]
        [SerializeField] private float delay;

        [Tooltip("Custom behaviours for the weapon.")]
        [SerializeField] private WeaponBehaviourData[] behaviourData;
        
        public ProjectileData ProjectileData => projectileData;
        public float Speed => speed;
        public float Delay => delay;
        public WeaponBehaviourData[] BehaviourData => behaviourData;
    }
}