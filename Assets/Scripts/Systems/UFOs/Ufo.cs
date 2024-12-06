using System;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Ufo : BaseEnemy, IDestructible, IPlayerCollideable
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
                // ChangeDirection();
            }
        }

        public void Setup(UfoData data)
        {
            isInitialised = false;
            
            this.data = data;
            cachedChangeDirectionInterval = data.ChangeDirectionInterval;

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
            
            if (data.Score > 0)
            {
                GiveScore(data.Score);
            }
        }

        public void GiveScore(uint score)
        {
            signalBus.TryFire(new ScoreAwardedEvent() { Score = score });
        }
    }
}