namespace Asteroids
{
    public interface IProjectileBehaviour
    {
        void Init(System.Action pushCallback);
        void Update();
        void OnCollision(UnityEngine.GameObject other);
    }
}