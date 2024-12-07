namespace Asteroids
{
    public struct AsteroidSpawnEvent
    {
        public AsteroidData AsteroidData;
        public uint NumberToSpawn;
        public UnityEngine.Vector2 Position;
        public UnityEngine.Vector2 Direction;
    }
}