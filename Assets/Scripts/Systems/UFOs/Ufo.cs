using System;
using Asteroids.Utils;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Ufo : BaseEnemy, IDestructible, IRemovable, IPlayerCollideable
    {
        [Inject] private SignalBus signalBus;

        private Rigidbody2D rigidbody2d;

        private UfoData data;
        private Action<Ufo> pushEvent;

        private bool isInitialised;
        private Vector2 originalDirection;
        private float cachedChangeDirectionInterval;
        
        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!isInitialised)
                return;
            
            cachedChangeDirectionInterval -= Time.deltaTime;
            if (cachedChangeDirectionInterval <= 0)
            {
                ChangeDirection();
            }
        }

        public void Setup(UfoData ufoData, Vector2 direction)
        {
            isInitialised = false;
            
            data = ufoData;
            originalDirection = direction;
            cachedChangeDirectionInterval = ufoData.ChangeDirectionInterval;
            
            Move(direction);

            isInitialised = true;
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

        private void ChangeDirection()
        {
            var newDirection = originalDirection + VectorUtils.GetRandomVectorWithinTolerance(data.ChangeDirectionTolerance);
            Move(newDirection.normalized);
            
            // Reset interval timer
            cachedChangeDirectionInterval = data.ChangeDirectionInterval;
        }

        private void Move(Vector2 direction)
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
            ReturnToPool();

            if (UnityEngine.Random.value <= data.ChanceOfDroppingPowerUp)
            {
                signalBus.TryFire(new PowerUpSpawnEvent() { Position = transform.position });
            }

            signalBus.TryFire(new UfoDestroyedEvent() { Position = transform.position});
            
            if (data.Score > 0)
            {
                signalBus.TryFire(new ScoreAwardedEvent() { Score = data.Score });
            }
        }

        public void RemoveSelf()
        {
            ReturnToPool();
            
            signalBus.TryFire<UfoRemovedSelfEvent>();
        }
    }
}