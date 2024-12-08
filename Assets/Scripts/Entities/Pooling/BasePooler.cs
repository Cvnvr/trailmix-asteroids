using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public abstract class BasePooler<T> : MonoBehaviour, IPooler<T> where T : MonoBehaviour, IPoolable<T>
    {
        public int PooledCount => inactiveObjects.Count;
        public int PushedCount => activeObjects.Count;
        public int TotalCount => PooledCount + PushedCount;
        
        [SerializeField] private PoolData poolData;

        private PoolData cachedPoolData;
        
        private List<T> inactiveObjects;
        private List<T> activeObjects;

        protected virtual void Awake()
        {
            inactiveObjects = new List<T>();
            activeObjects = new List<T>();
        }

        protected virtual void Start()
        {
            InitPoolData();
            Prefill();
        }

        protected void InitPoolData()
        {
            cachedPoolData = poolData;
        }

        protected void SetPoolData(PoolData data)
        {
            cachedPoolData = data;
            Clear();
            Prefill();
        }

        protected void Prefill()
        {
            for (var i = 0; i < cachedPoolData.InitialPoolSize; i++)
            {
                var obj = CreateObject();
                Push(obj);
            }
        }
        
        public T Pop()
        {
            T obj;
            if (PooledCount > 0)
            {
                obj = inactiveObjects[inactiveObjects.Count - 1];
                inactiveObjects.RemoveAt(inactiveObjects.Count - 1);
            }
            else
            {
                obj = CreateObject();
            }
    
            obj.InitPoolable(Push);
            activeObjects.Add(obj);
            ActivateObject(obj);
            return obj;
        }

        public T Pop(Vector2 position, Quaternion rotation)
        {
            T obj;
            if (PooledCount > 0)
            {
                obj = inactiveObjects[inactiveObjects.Count - 1];
                inactiveObjects.RemoveAt(inactiveObjects.Count - 1);
            }
            else
            {
                obj = CreateObject();
            }
            
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            
            obj.InitPoolable(Push);
            activeObjects.Add(obj);
            ActivateObject(obj);
            return obj;
        }
        
        public virtual void Push()
        {
            var obj = activeObjects[activeObjects.Count - 1];
            if (obj)
            {
                Push(obj);
            }
        }

        public virtual void Push(T obj)
        {
            DeactivateObject(obj);
            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
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
            foreach (var obj in inactiveObjects)
            {
                DestroyObject(obj);
            }
            foreach (var obj in activeObjects)
            {
                DestroyObject(obj);
            }
    
            inactiveObjects.Clear();
            activeObjects.Clear();
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