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
        
        private bool doesSpawnMoreOnDestruction;
        private int numberToSpawn;
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

            rigidbody2d.velocity = rigidbody2d.velocity.normalized * movementSpeed;
            var direction = transform.up * movementSpeed;
            //rigidbody2d.AddForce(direction, ForceMode2D.Force);
        }

        public void Setup(AsteroidData data)
        {
            isInitialised = false;
            
            spriteRenderer.sprite = data.Sprites[UnityEngine.Random.Range(0, data.Sprites.Count)];
            movementSpeed = data.MovementSpeed;
            
            doesSpawnMoreOnDestruction = data.DoesSpawnMoreOnDestruction;
            numberToSpawn = data.NumberToSpawn;
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
            if (doesSpawnMoreOnDestruction)
            {
                signalBus.TryFire(new AsteroidSpawnEvent()
                {
                    AsteroidData = spawnedAsteroidData,
                    NumberToSpawn = numberToSpawn,
                    Position = transform.position
                });
            }
            
            ReturnToPool();
        }
    }
}