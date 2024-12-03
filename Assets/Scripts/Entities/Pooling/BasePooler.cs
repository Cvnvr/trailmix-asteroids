using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public abstract class BasePooler<T> : MonoBehaviour, IPooler<T> where T : MonoBehaviour, IPoolable<T>
    {
        public int PooledCount => pooledObjects.Count;
        public int PushedCount => pushedObjects.Count;
        public int TotalCount => PooledCount + PushedCount;
        
        [SerializeField] private PoolData poolData;

        private List<T> pooledObjects;
        private List<T> pushedObjects;

        protected virtual void Awake()
        {
            pooledObjects = new List<T>();
        }

        protected virtual void Start()
        {
            Prefill();
        }

        private void Prefill()
        {
            if (pooledObjects == null || pooledObjects.Count != poolData.InitialPoolSize)
            {
                pooledObjects = new List<T>(poolData.InitialPoolSize);
                pushedObjects = new List<T>(poolData.InitialPoolSize);
            }
            else
            {
                var newCapacity = pooledObjects.Count + poolData.InitialPoolSize;
                pooledObjects.Capacity = newCapacity;
                pushedObjects.Capacity = newCapacity;
            }
            
            for (int i = 0; i < poolData.InitialPoolSize; i++)
            {
                T obj = CreateObject();
                Push(obj);
            }
        }
        
        public T Pop()
        {
            T obj;
            var count = PooledCount;
            if (count > 0)
            {
                obj = pooledObjects[count - 1];
                ActivateObject(obj);
                pooledObjects.RemoveAt(count - 1);
            }
            else if (TotalCount < poolData.MaxPoolSize)
            {
                // Create new object if there are no more left in the pool
                obj = CreateObject();
            }
            else
            {
                // Reached max pool count
                return null;
            }
    
            obj.InitPoolable(Push);
            pushedObjects.Add(obj);
            return obj;
        }

        public T Pop(Vector3 position, Quaternion rotation)
        {
            T obj = Pop();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }
            return obj;
        }

        public virtual void Push(T obj)
        {
            DeactivateObject(obj);
            pooledObjects.Add(obj);
        }

        protected abstract T CreateObject();

        protected virtual void ActivateObject(T obj)
        {
            obj.gameObject.SetActive(true);
            obj.OnPoolableActivated();
        }

        protected virtual void DeactivateObject(T obj)
        {
            obj.OnPoolableDeactivated();
            obj.gameObject.SetActive(false);
        }
        
        public void Clear()
        {
            foreach (T obj in pooledObjects)
            {
                DestroyObject(obj);
            }
            foreach (T obj in pushedObjects)
            {
                DestroyObject(obj);
            }
    
            pooledObjects.Clear();
            pushedObjects.Clear();
        }

        protected virtual void DestroyObject(T obj)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}