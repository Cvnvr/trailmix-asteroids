using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroySelfAfterTimeComponent : IProjectileBehaviour
    {
        private Action pushEvent;
        private float lifetime;
        
        public void Setup(Action pushCallback, float lifetime)
        {
            Init(pushCallback);
            this.lifetime = lifetime;
        }

        public void Init(Action pushCallback)
        {
            pushEvent = pushCallback;
        }

        public void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                pushEvent?.Invoke();
            }
        }

        public void OnCollision(GameObject other)
        {
        }
    }
}