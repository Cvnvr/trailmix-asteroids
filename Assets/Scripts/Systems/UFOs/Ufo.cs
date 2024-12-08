using System;
using Asteroids.Utils;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Ufo : BaseEnemy, IDestructible, IRemovable, IPlayerCollideable
    {
        [Inject] private PlayerLocator playerLocator;
        [Inject] private SignalBus signalBus;

        private Rigidbody2D rigidbody2d;

        private UfoData data;
        private Action<Ufo> pushEvent;

        private Action<ProjectileSpawnData> fireProjectileEvent;

        private bool isInitialised;
        private Vector2 originalDirection;
        
        private float cachedChangeDirectionInterval;
        private float cachedTimeBetweenShots;
        
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

            cachedTimeBetweenShots -= Time.deltaTime;
            if (cachedTimeBetweenShots <= 0)
            {
                Shoot();
                cachedTimeBetweenShots = data.TimeBetweenShots;
            }
        }

        public void Setup(UfoData ufoData, Vector2 direction, Action<ProjectileSpawnData> fireProjectileCallback)
        {
            isInitialised = false;
            
            data = ufoData;
            originalDirection = direction;
            fireProjectileEvent = fireProjectileCallback;

            cachedChangeDirectionInterval = ufoData.ChangeDirectionInterval;
            cachedTimeBetweenShots = ufoData.TimeBetweenShots;
            
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

        private void Shoot()
        {
            if (!playerLocator.TryGetPlayerPosition(out var playerPosition))
                return;

            var directionToPlayer = (playerPosition - (Vector2)transform.position).normalized;
            
            // Offset the spawn position to prevent collision with the UFO itself
            var spawnPosition = (Vector2)transform.position + directionToPlayer * 1.0f;
            
            fireProjectileEvent?.Invoke(new ProjectileSpawnData()
            {
                Position = spawnPosition,
                Rotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer),
                Direction = directionToPlayer
            });
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