using UnityEngine;

namespace Asteroids.Utils
{
    public static class VectorUtils
    {
        public static Vector2 GetRandomVectorWithinTolerance(float tolerance)
        {
            return new Vector2(
                Random.Range(-tolerance, tolerance), 
                Random.Range(-tolerance, tolerance)
            );
        }
    }
}