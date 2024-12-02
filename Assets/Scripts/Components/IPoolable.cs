namespace Components
{
    public interface IPoolable
    {
        void Initialise(System.Action<IPoolable> returnToPool);
        void OnObjectSpawned();
        void OnObjectDespawned();
        void ReturnToPool();
    }
}