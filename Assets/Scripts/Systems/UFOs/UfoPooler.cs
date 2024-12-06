using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class UfoPooler : BasePooler<Ufo>
    {
        [Inject] private DiContainer container;

        private UfoData ufoData;

        protected override void Start()
        {
            // Do nothing as Init() handles prefilling
        }

        public void Init(UfoData data, PoolData poolData)
        {
            ufoData = data;
            SetPoolData(poolData);
        }
        
        protected override Ufo CreateObject()
        {
            return container.InstantiatePrefab(
                ufoData.Prefab, 
                transform.position, 
                Quaternion.identity, 
                transform
            ).GetComponent<Ufo>(); 
        }

        protected override void ActivateObject(Ufo ufo)
        {
            base.ActivateObject(ufo);
            ufo.Setup(ufoData);
            ufo.Move(ufo.transform.up);
        }
    }
}