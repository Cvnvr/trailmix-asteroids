using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPoolController : MonoBehaviour
    {
        [SerializeField] private PoolData poolData;
        [SerializeField] private AsteroidData[] asteroidData;

        [Inject] private DiContainer container;
        [Inject] private SignalBus signalBus;
        
        private Dictionary<AsteroidType, AsteroidPooler> pools;

        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
        }
        
        private void Awake()
        {
            pools = new();
            foreach (var data in asteroidData)
            {
                var pool = container.InstantiateComponent<AsteroidPooler>(gameObject);
                pool.Init(data, poolData);
                pools.Add(data.AsteroidType, pool);
            }
        }

        private void OnAsteroidSpawn(AsteroidSpawnEvent evt)
        {
            if (evt.AsteroidData == null)
                return;
            
            if (!pools.TryGetValue(evt.AsteroidData.AsteroidType, out var pool)) 
                return;
            
            for (var i = 0; i < evt.NumberToSpawn; i++)
            {
                pool.Pop(evt.Position, Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
        }
    }
}