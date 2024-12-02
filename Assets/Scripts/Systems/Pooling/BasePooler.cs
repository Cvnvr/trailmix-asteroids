using Components;
using Entities.Pooling;
using UnityEngine;
using UnityEngine.Pool;

namespace Systems.Pooling
{
    public abstract class BasePooler<T> : MonoBehaviour, IPooler<T> where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private PoolData poolData;

        private ObjectPool<T> pool;

        protected virtual void Awake()
        {
            pool = new ObjectPool<T>(
                CreateItem,
                OnGet,
                OnRelease,
                OnDestroyItem,
                poolData.CollectionCheck,
                poolData.InitialPoolSize,
                poolData.MaxPoolSize
            );
        }

        protected abstract T CreateItem();

        protected virtual void OnGet(T item)
        {
            item.gameObject.SetActive(true);
            item.OnObjectSpawned();
        }

        protected virtual void OnRelease(T item)
        {
            item.OnObjectDespawned();
            item.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyItem(T item)
        {
            Destroy(item.gameObject);
        }

        public virtual T Pop()
        {
            return pool.Get();
        }

        public virtual void Push(T poolable)
        {
            pool.Release(poolable);
        }
    }
}