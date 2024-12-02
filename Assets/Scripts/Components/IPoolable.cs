namespace Components
{
    public interface IPoolable
    {
        void Initialise(System.Action<IPoolable> pushCallback);
        void OnObjectSpawned();
        void OnObjectDespawned();
        void ReturnToPool();
    }
}