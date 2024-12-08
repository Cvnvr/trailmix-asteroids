using System;
using Asteroids.Utils;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Asteroid : BaseEnemy, IDestructible, IPlayerCollideable
    {
        [Inject] private SignalBus signalBus;
        
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidbody2d;

        private AsteroidData data;
        private Action<Asteroid> pushEvent;
        
        private Vector2 originalDirection;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void Setup(AsteroidData data, Vector2 direction)
        {
            this.data = data;
            spriteRenderer.sprite = data.Sprites[UnityEngine.Random.Range(0, data.Sprites.Length)];

            originalDirection = (direction + VectorUtils.GetRandomVectorWithinTolerance(1f)).normalized;
            Move(originalDirection);
        }
        
        public void InitPoolable(Action<Asteroid> pushCallback)
        {
            pushEvent = pushCallback;
        }

        public override void InitPoolable(Action<BaseEnemy> pushCallback)
        {
            pushEvent = pushCallback;
        }

        public override void OnPoolableActivated()
        {
        }

        public override void OnPoolableDeactivated()
        {
        }

        public override void ReturnToPool()
        {
            pushEvent?.Invoke(this);
        }
        
        public void Move(Vector2 direction)
        {
            rigidbody2d.velocity = direction * data.MovementSpeed;
        }

        public void OnPlayerCollision(GameObject player)
        {
            Destroy(player);
            signalBus.TryFire<PlayerDestroyedEvent>();
            
            Destroy();
        }
        
        public void Destroy()
        {
            signalBus.TryFire(new AsteroidDestroyedEvent()
            {
                AsteroidData = data,
                Position = transform.position,
                Direction = originalDirection
            });

            if (data.Score > 0)
            {
                signalBus.TryFire(new ScoreAwardedEvent() { Score = data.Score });
            }
            
            ReturnToPool();
        }
    }
}