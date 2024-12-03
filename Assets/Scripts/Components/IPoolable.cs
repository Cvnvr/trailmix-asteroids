namespace Components
{
    public interface IPoolable<T>
    {
        void InitPoolable(System.Action<T> pushCallback);
        void OnPoolableActivated();
        void OnPoolableDeactivated();
        void ReturnToPool();
    }
}