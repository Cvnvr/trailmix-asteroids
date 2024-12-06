using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class EnemyPooler : BasePooler<BaseEnemy>
    {
        [Inject] private DiContainer container;

        private EnemyData enemyData;

        protected override void Start()
        {
            // Do nothing as Init() handles prefilling
        }

        public void Init(EnemyData data, PoolData poolData)
        {
            enemyData = data;
            SetPoolData(poolData);
        }
        
        protected override BaseEnemy CreateObject()
        {
            return container.InstantiatePrefab(
                enemyData.Prefab, 
                transform.position, 
                Quaternion.identity, 
                transform
            ).GetComponent<BaseEnemy>(); 
        }

        protected override void ActivateObject(BaseEnemy enemy)
        {
            base.ActivateObject(enemy);
        }
    }
}