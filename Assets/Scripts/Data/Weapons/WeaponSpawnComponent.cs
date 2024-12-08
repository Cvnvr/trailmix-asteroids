using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Asteroids
{
    public struct WeaponSpawnComponent
    {
        public Transform SpawnTransform;
        public System.Action<ProjectileSpawnData> PopCallback;
    }
}