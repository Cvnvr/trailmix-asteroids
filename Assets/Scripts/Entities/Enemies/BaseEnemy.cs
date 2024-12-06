using System;
using UnityEngine;

namespace Asteroids
{
    public abstract class BaseEnemy : MonoBehaviour, IPoolable<BaseEnemy>
    {
        public abstract void InitPoolable(Action<BaseEnemy> pushCallback);
        public abstract void OnPoolableActivated();
        public abstract void OnPoolableDeactivated();
        public abstract void ReturnToPool();
    }
}