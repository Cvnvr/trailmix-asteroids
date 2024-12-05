using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidPooler : BasePooler<Asteroid>
    {
        [Inject] private DiContainer container;

        private AsteroidData asteroidData;

        protected override void Start()
        {
            // Do nothing as Init() handles prefilling
        }

        public void Init(AsteroidData data, PoolData poolData)
        {
            asteroidData = data;
            SetPoolData(poolData);
        }
        
        protected override Asteroid CreateObject()
        {
            return container.InstantiatePrefab(
                asteroidData.Prefab, 
                transform.position, 
                Quaternion.identity, 
                transform
            ).GetComponent<Asteroid>(); 
        }

        protected override void ActivateObject(Asteroid asteroid)
        {
            base.ActivateObject(asteroid);
            asteroid.Setup(asteroidData);
            asteroid.Move(asteroid.transform.up);
        }
    }
}