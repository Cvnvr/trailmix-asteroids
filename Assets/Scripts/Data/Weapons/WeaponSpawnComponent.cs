using UnityEngine;

namespace Asteroids
{
    public struct WeaponSpawnComponent
    {
        public Transform SpawnTransform;
        public System.Action<ProjectileSpawnData> PopCallback;
    }
}