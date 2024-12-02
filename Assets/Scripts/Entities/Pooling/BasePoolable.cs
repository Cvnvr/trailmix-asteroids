using System;
using Components;
using UnityEngine;

namespace Entities.Pooling
{
    public abstract class BasePoolable : MonoBehaviour, IPoolable
    {
        public abstract void Initialise(Action<IPoolable> returnToPool);
        public abstract void OnObjectSpawned();
        public abstract void OnObjectDespawned();
        public abstract void ReturnToPool();
    }
}