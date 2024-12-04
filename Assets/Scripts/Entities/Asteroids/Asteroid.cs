using System;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    public class Asteroid : MonoBehaviour, IPoolable<Asteroid>, IDestructible, IPlayerCollideable
    {
        [Inject] private SignalBus signalBus;
        
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidbody2d;

        private Action<Asteroid> pushEvent;

        private float movementSpeed;
        
        private bool canSpawnMore;
        private AsteroidData spawnedAsteroidData;

        private bool isInitialised;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!isInitialised)
                return;

            if (rigidbody2d != null)
            {
                rigidbody2d.velocity = transform.up * movementSpeed;
            }
        }

        public void Setup(AsteroidData data)
        {
            isInitialised = false;
            
            spriteRenderer.sprite = data.Sprites[UnityEngine.Random.Range(0, data.Sprites.Count)];
            movementSpeed = data.MovementSpeed;
            
            canSpawnMore = data.CanSpawnMore;
            spawnedAsteroidData = data.SpawnedAsteroidData;
            
            isInitialised = true;
        }
        
        public void InitPoolable(Action<Asteroid> pushCallback)
        {
            pushEvent = pushCallback;
        }

        public void OnPoolableActivated()
        {
            // Randomise the asteroid's rotation
            var rotation = UnityEngine.Random.Range(0f, 361f);
            var currentRotation = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation + rotation));
        }

        public void OnPoolableDeactivated()
        {
            isInitialised = false;
        }

        public void ReturnToPool()
        {
            pushEvent?.Invoke(this);
        }

        public void Destroy()
        {
            if (canSpawnMore)
            {
                signalBus.TryFire(new AsteroidSpawnMoreEvent() { AsteroidData = spawnedAsteroidData });
            }
            
            ReturnToPool();
        }

        public void OnPlayerCollision(GameObject player)
        {
            Destroy(player);
            signalBus.TryFire<PlayerDestroyedEvent>();
            
            Destroy();
        }
    }
}