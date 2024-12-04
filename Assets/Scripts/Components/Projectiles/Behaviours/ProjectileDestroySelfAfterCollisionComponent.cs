using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroySelfAfterCollisionComponent : IProjectileBehaviour
    {
        private Action pushEvent;
        
        public void Init(Action pushCallback)
        {
            pushEvent = pushCallback;
        }

        public void Update()
        {
        }

        public void OnCollision(GameObject collision)
        {
            if (collision.CompareTag(EntityTags.Projectile))
                return;
            
            pushEvent?.Invoke();
        }
    }
}