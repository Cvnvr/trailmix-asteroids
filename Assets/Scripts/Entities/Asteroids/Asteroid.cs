using System;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Asteroid : MonoBehaviour, IPoolable<Asteroid>, IPlayerCollideable
    {
        [Inject] private SignalBus signalBus;
        
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidbody2d;

        private AsteroidData data;
        private Action<Asteroid> pushEvent;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void Setup(AsteroidData data)
        {
            this.data = data;
            spriteRenderer.sprite = data.Sprites[UnityEngine.Random.Range(0, data.Sprites.Count)];
        }
        
        public void InitPoolable(Action<Asteroid> pushCallback)
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

            signalBus.TryFire(new AsteroidDestroyedEvent()
            {
                AsteroidData = data,
                Position = transform.position
            });

            if (data.Score > 0)
            {
                signalBus.TryFire(new ScoreAwardedEvent() { Score = data.Score });
            }
        }
    }
}