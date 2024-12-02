using System;
using System.Collections;
using Components;
using Entities.Pooling;
using UnityEngine;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : BasePoolable, IProjectile
    {
        private BaseProjectileData data;
        private Rigidbody2D rigidbody2d;
        private Action<IPoolable> returnEvent;
        
        public void Init(BaseProjectileData projectileData, Action<IPoolable> returnToPool)
        {
            data = projectileData;
            Initialise(returnToPool);
        }
        
        public override void Initialise(Action<IPoolable> returnToPool)
        {
            returnEvent = returnToPool;
        }

        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public override void OnObjectSpawned()
        {
            // TODO fireSound.Play();

            if (data.Lifetime > 0)
            {
                StartCoroutine(DestroySelfAfterDelay());
            }
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
            if (rigidbody2d == null)
                return;
            
            rigidbody2d.velocity = velocity * data.Speed;
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
        
        private IEnumerator DestroySelfAfterDelay()
        {
            yield return new WaitForSeconds(data.Lifetime);
            ReturnToPool();
        }
    }
}