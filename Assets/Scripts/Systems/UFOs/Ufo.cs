using System;
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

        public void Setup(UfoData ufoData)
        {
            isInitialised = false;
            
            data = ufoData;
            cachedChangeDirectionInterval = ufoData.ChangeDirectionInterval;

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
            // TODO make new direction within tolerance of original direction
            
            var newDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            Move(newDirection.normalized);
            
            // Reset interval timer
            cachedChangeDirectionInterval = data.ChangeDirectionInterval;
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