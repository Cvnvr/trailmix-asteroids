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

    public struct ProjectileSpawnData
    {
        public Vector2 Position;
        public Quaternion Rotation;
        public Vector2 Direction;
    }
}