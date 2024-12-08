using System.Collections.Generic;
using Asteroids.Utils;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class UfoPoolController : MonoBehaviour
    {
        [Header("UFOs")]
        [SerializeField] private PoolData poolData;
        [SerializeField] private UfoData[] ufoData;

        [Header("UFO Projectiles")]
        [SerializeField] private PoolData projectilePoolData;
        [SerializeField] private WeaponData weaponData;
        
        [Inject] private DiContainer container;
        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;
        [Inject] private SignalBus signalBus;

        private Dictionary<UfoType, EnemyPooler> pools;
        private UfoProjectilePooler projectilePool;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<UfoSpawnEvent>(OnTrySpawnUfo);
        }

        private void Awake()
        {
            pools = new();
            foreach (var data in ufoData)
            {
                var pool = container.InstantiateComponent<EnemyPooler>(gameObject);
                pool.Init(data, poolData);
                pools.Add(data.UfoType, pool);
            }
            
            projectilePool = container.InstantiateComponent<UfoProjectilePooler>(gameObject);
            projectilePool.Init(weaponData, poolData);
        }

        private void OnTrySpawnUfo(UfoSpawnEvent evt)
        {
            var randomUfoData = ufoData[Random.Range(0, ufoData.Length)];

            if (pools == null || !pools.TryGetValue(randomUfoData.UfoType, out var pool))
            {
                Debug.LogWarning($"[{nameof(UfoPoolController)}.{nameof(OnTrySpawnUfo)}] Failed to spawn a UFO");
                evt.SuccessCallback?.Invoke(false);
                return;
            }
            
            var randomSpawnPosition = screenBoundsCalculator.GetRandomPaddedScreenPosition();
            var ufo = (Ufo)pool.Pop(randomSpawnPosition, Quaternion.identity);
            if (!ufo)
            {
                Debug.LogWarning($"[{nameof(UfoPoolController)}.{nameof(OnTrySpawnUfo)}] Failed to spawn a UFO");
                evt.SuccessCallback?.Invoke(false);
                return;
            }
            
            var randomDirection = GetRandomDirection(randomSpawnPosition, randomUfoData.SpawnDirectionTolerance);
            ufo.Setup(randomUfoData, randomDirection, projectilePool.SpawnProjectile);
                
            evt.SuccessCallback?.Invoke(true);
        }
        
        private Vector2 GetRandomDirection(Vector2 position, float tolerance)
        {
            var direction = (screenBoundsCalculator.GetCenterOfScreen() - position).normalized;
            direction += VectorUtils.GetRandomVectorWithinTolerance(tolerance);
            return direction.normalized;
        }
        
        private void OnDisable()
        {
            signalBus.TryUnsubscribe<UfoSpawnEvent>(OnTrySpawnUfo);
        }
    }
}