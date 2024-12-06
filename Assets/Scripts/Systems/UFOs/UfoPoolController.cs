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
            signalBus.Subscribe<UfoSpawnEvent>(OnUfoSpawn);
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

        private void OnUfoSpawn(UfoSpawnEvent evt)
        {
            if (evt.UfoData == null)
            {
                Debug.LogWarning($"[{nameof(UfoPoolController)}.{nameof(OnUfoSpawn)}] Attempted to spawn a UFO with invalid data");
                return;
            }
            
            if (!pools.TryGetValue(evt.UfoData.UfoType, out var pool)) 
                return;
            
            var ufo = pool.Pop(evt.Position, Quaternion.identity) as Ufo;
            if (ufo)
            {
                ufo.Setup(evt.UfoData);
                ufo.Move(ufo.transform.up);
            }
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<UfoSpawnEvent>(OnUfoSpawn);
        }
    }
}