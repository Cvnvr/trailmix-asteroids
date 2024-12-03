using UnityEngine;

namespace Asteroids
{
    public interface IWeaponBehaviourFactory
    {
        IWeaponBehaviour GetBoundComponent(WeaponSpawnData spawnData, WeaponBehaviourData weaponBehaviourData);
    }
}