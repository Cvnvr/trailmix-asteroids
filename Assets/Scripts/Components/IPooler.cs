namespace Components
{
    public interface IPooler<T> where T : IPoolable
    {
        T Pop();
        void Push(T poolable);
    }
}