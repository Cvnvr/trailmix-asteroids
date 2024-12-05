using System;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Ufo : MonoBehaviour, IPoolable<Ufo>, IMoveable, IDestructible, IPlayerCollideable, IScoreGiver
    {
        [Inject] private SignalBus signalBus;

        private UfoData data;
        private Rigidbody2D rigidbody2d;

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
        
        public void InitPoolable(Action<Ufo> pushCallback)
        {
            pushEvent = pushCallback;
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

        public void Move(Vector2 direction)
        {
            rigidbody2d.velocity = direction * data.MovementSpeed;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(EntityTags.Projectile))
            {
                Destroy();
            }
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

        public void GiveScore(int score)
        {
            signalBus.TryFire(new ScoreAwardedEvent() { Score = score });
        }
    }
}