using System;
using UnityEngine;

namespace Asteroids
{
    public class LifeUIElement : MonoBehaviour, IPoolable<LifeUIElement>
    {
        public void InitPoolable(Action<LifeUIElement> pushCallback)
        {
        }

        public void OnPoolableActivated()
        {
        }

        public void OnPoolableDeactivated()
        {
        }

        public void ReturnToPool()
        {
        }
    }
}