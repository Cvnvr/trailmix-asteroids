using System;
using System.Collections.Generic;
using Components;
using Systems.Projectiles;
using UnityEngine;
using Zenject;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Projectile : MonoBehaviour, Components.IPoolable<Projectile>, IProjectile
    {
        [Inject] private IProjectileBehaviourFactory projectileBehaviourFactory;

        private Rigidbody2D rigidbody2d;
        private Dictionary<ProjectileBehaviourData, BaseProjectileBehaviourComponent> behaviours;

        private ProjectileData projectileData;
        private Action<Projectile> pushEvent;
        
        public void InitPoolable(Action<Projectile> pushCallback)
        {
            pushEvent = pushCallback;
        }
        
        public void SetProjectileData(ProjectileData projectileData)
        {
            this.projectileData = projectileData;
            
            behaviours = new();
            foreach (var behaviourData in projectileData.Behaviours)
            {
                if (behaviours.ContainsKey(behaviourData))
                    continue;

                var behaviourComponent = projectileBehaviourFactory.GetBoundComponent(this, behaviourData);
                if (behaviourComponent != null)
                {
                    behaviours.Add(behaviourData, behaviourComponent);
                }
            }
        }
        
        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Value.Update();
            }
        }

        public void OnPoolableActivated()
        {
            // TODO fireSound.Play();
        }

        public void OnPoolableDeactivated()
        {
        }

        public void ReturnToPool()
        {
            pushEvent?.Invoke(this);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.Value.OnCollision(collision.gameObject);
            }
        }
        
        public void Fire(Vector2 velocity)
        {
            if (rigidbody2d != null)
            {
                rigidbody2d.velocity = velocity * projectileData.Speed;
            }            
        }
    }
}