namespace Asteroids
{
    public struct AsteroidDestroyedEvent
    {
        public AsteroidData AsteroidData;
        public UnityEngine.Vector2 Position;
        public UnityEngine.Vector2 Direction;
    }
}