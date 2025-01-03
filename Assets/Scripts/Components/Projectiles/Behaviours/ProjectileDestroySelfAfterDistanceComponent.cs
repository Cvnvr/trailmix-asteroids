using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroySelfAfterDistanceComponent : IProjectileBehaviour
    {
        private Action pushEvent;
        private Vector2 originalPosition;
        private float distance;

        private Projectile projectile;
        
        public void Setup(Projectile projectile, Action pushCallback, float distance)
        {
            Init(pushCallback);
            this.projectile = projectile;
            this.distance = distance;
            
            originalPosition = projectile.transform.position;
        }

        public void Init(Action pushCallback)
        {
            pushEvent = pushCallback;
        }

        public void Update()
        {
            if (projectile == null)
                return;
            
            if (Vector2.Distance(originalPosition, projectile.transform.position) >= distance)
            {
                pushEvent?.Invoke();
            }
        }

        public void OnCollision(GameObject other)
        {
        }
    }
}