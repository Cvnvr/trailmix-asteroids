namespace Asteroids
{
    public struct UfoSpawnEvent
    {
        public UnityEngine.Vector2 Position;
        public System.Action<bool> SuccessCallback;
    }
}