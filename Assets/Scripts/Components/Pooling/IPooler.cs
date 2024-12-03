namespace Asteroids
{
    public interface IPooler<T>
    {
        T Pop();
        void Push(T obj);
    }
}