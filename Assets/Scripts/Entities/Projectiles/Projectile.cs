using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        [Inject] private IProjectileBehaviourFactory projectileBehaviourFactory;

        private Rigidbody2D rigidbody2d;
        private Dictionary<ProjectileBehaviourData, IProjectileBehaviour> behaviours;

        private ProjectileData projectileData;
        private Action<Projectile> pushEvent;
        
        public void InitPoolable(Action<Projectile> pushCallback)
        {
            pushEvent = pushCallback;
        }
        
        public void SetProjectileData(ProjectileData data, Vector2 direction)
        {
            projectileData = data;
            
            behaviours = new();
            foreach (var behaviourData in projectileData.Behaviours)
            {
                if (behaviours.ContainsKey(behaviourData))
                    continue;

                var behaviourComponent = projectileBehaviourFactory.GetBoundComponent(this, ReturnToPool, behaviourData);
                if (behaviourComponent != null)
                {
                    behaviours.Add(behaviourData, behaviourComponent);
                }
            }
            
            Fire(direction);
        }
        
        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            if (behaviours == null || behaviours.Count == 0)
                return;
            
            foreach (var behaviour in behaviours)
            {
                behaviour.Value.Update();
            }
        }

        public void OnPoolableActivated()
        {
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
            if (behaviours == null || behaviours.Count == 0)
                return;
            
            foreach (var behaviour in behaviours)
            {
                behaviour.Value.OnCollision(collision.gameObject);
            }
        }
        
        private void Fire(Vector2 direction)
        {
            rigidbody2d.velocity = direction;
        }
    }
}