using System;
using System.Collections.Generic;
using System.Linq;
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

        private ProjectileData projectileData;
        
        private Rigidbody2D rigidbody2d;

        private Action<IPoolable> returnEvent;

        public void SetData(ProjectileData projectileData)
        {
            this.projectileData = projectileData;
        }
        
        public void Initialise(Action<IPoolable> returnToPool)
        {
            returnEvent = returnToPool;

            foreach (var behaviour in projectileData.Behaviours)
            {
                projectileBehaviourFactory.BindTo(this, behaviour);
            }
        }
        
        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
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
            returnEvent?.Invoke(this);
        }
        
        public void Fire(Vector2 velocity)
        {
            if (rigidbody2d == null)
                return;
            
            rigidbody2d.velocity = velocity * projectileData.Speed;
        }
    }
}