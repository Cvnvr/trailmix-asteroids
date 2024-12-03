using System;
using System.Collections.Generic;
using Components;
using Systems.Projectiles;
using UnityEngine;
using Zenject;
using IPoolable = Components.IPoolable;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Projectile : MonoBehaviour, IPoolable, IProjectile
    {
        [Inject] private IProjectileBehaviourFactory projectileBehaviourFactory;

        private Rigidbody2D rigidbody2d;
        private Dictionary<ProjectileBehaviourData, BaseProjectileBehaviourComponent> behaviours;

        private ProjectileData projectileData;
        private Action<IPoolable> pushEvent;
        
        public void SetData(ProjectileData projectileData)
        {
            this.projectileData = projectileData;
        }
        
        public void Initialise(Action popCallback, Action<IPoolable> pushCallback)
        {
            pushEvent = pushCallback;

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

        public void OnObjectSpawned()
        {
            // TODO fireSound.Play();
        }

        public void OnObjectDespawned()
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