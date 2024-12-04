using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPoolManager : MonoBehaviour
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
        
        private void Start()
        {
            pools = new();
            foreach (var data in asteroidData)
            {
                var pool = container.InstantiateComponent<AsteroidPooler>(gameObject);
                pool.Init(data, poolData);
                pools.Add(data.AsteroidType, pool);
            }

            // TODO delete me
            for (int i = 0; i < 5; i++)
            {
                signalBus.Fire(new AsteroidSpawnEvent()
                {
                    AsteroidData = asteroidData[0],
                    NumberToSpawn = 1,
                    Position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0),
                });
            }
        }

        private void OnAsteroidSpawn(AsteroidSpawnEvent evt)
        {
            if (!pools[evt.AsteroidData.AsteroidType])
                return;

            for (var i = 0; i < evt.NumberToSpawn; i++)
            {
                pools[evt.AsteroidData.AsteroidType].Pop(evt.Position, Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            signalBus.TryUnsubscribe<AsteroidSpawnEvent>(OnAsteroidSpawn);
        }
    }
}