using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Entities/Weapons/WeaponData", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Tooltip("The data for the projectile that this weapon fires.")]
        [SerializeField] private ProjectileData projectileData;
        
        [Tooltip("Delay before another projectile can be fired.")]
        [SerializeField] private float delay;

        [Tooltip("Custom behaviours for the weapon.")]
        [SerializeField] private WeaponBehaviourData[] behaviourData;
        
        public ProjectileData ProjectileData => projectileData;
        public float Delay => delay;
        public WeaponBehaviourData[] BehaviourData => behaviourData;
    }
}