using System;
using Components;
using Entities.Pooling;
using UnityEngine;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : BasePoolable, IProjectile
    {
        private Action<IPoolable> returnEvent;
        
        public override void Initialise(Action<IPoolable> returnToPool)
        {
            returnEvent = returnToPool;
        }
        
        public override void OnObjectSpawned()
        {
            // TODO fireSound.Play();
        }

        public override void OnObjectDespawned()
        {
        }

        public override void ReturnToPool()
        {
            returnEvent?.Invoke(this);
        }
        
        public void Fire(Vector2 velocity)
        {
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage();

                ReturnToPool();
            }
        }
    }
}