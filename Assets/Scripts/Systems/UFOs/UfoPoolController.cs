using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class UfoPoolController : MonoBehaviour
    {
        [SerializeField] private PoolData poolData;
        [SerializeField] private UfoData[] ufoData;
        
        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;

        private Dictionary<UfoType, EnemyPooler> pools;
        
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
        }

        private void OnTrySpawnUfo(UfoSpawnEvent evt)
        {
            var randomUfoData = ufoData[Random.Range(0, ufoData.Length)];

            if (!pools.TryGetValue(randomUfoData.UfoType, out var pool))
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
            
            ufo.Setup(randomUfoData);
            ufo.Move(ufo.transform.up);
                
            evt.SuccessCallback?.Invoke(true);
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<UfoSpawnEvent>(OnTrySpawnUfo);
        }
    }
}