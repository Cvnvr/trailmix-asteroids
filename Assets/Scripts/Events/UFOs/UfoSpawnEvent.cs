namespace Asteroids
{
    public struct UfoSpawnEvent
    {
        public UnityEngine.Vector2 Position;
        public UnityEngine.Vector2 Direction;
        public System.Action<bool> SuccessCallback;
    }
}