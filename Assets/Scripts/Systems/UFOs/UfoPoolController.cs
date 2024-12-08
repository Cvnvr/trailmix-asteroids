using System.Collections.Generic;
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
                evt.SuccessCallback?.Invoke(false);
                return;
            }
            
            var ufo = pool.Pop(evt.Position, Quaternion.identity) as Ufo;
            if (!ufo)
            {
                evt.SuccessCallback?.Invoke(false);
                return;
            }
            
            ufo.Setup(randomUfoData, evt.Direction, projectilePool.SpawnProjectile);
                
            evt.SuccessCallback?.Invoke(true);
        }
        
        private void OnDisable()
        {
            signalBus.TryUnsubscribe<UfoSpawnEvent>(OnTrySpawnUfo);
        }
    }
}