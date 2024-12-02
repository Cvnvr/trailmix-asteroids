using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using UnityEngine;

namespace Entities.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Projectile : MonoBehaviour, IPoolable, IProjectile
    {
        [SerializeField] private float speed;
        
        private Rigidbody2D rigidbody2d;
        private List<IProjectileBehaviour> behaviours;

        private Action<IPoolable> returnEvent;
        
        public void Initialise(Action<IPoolable> returnToPool)
        {
            returnEvent = returnToPool;

            behaviours = GetComponents<IProjectileBehaviour>().ToList();
            foreach (var behaviour in behaviours)
            {
                behaviour.Init(this);
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
            
            rigidbody2d.velocity = velocity * speed;
        }
    }
}