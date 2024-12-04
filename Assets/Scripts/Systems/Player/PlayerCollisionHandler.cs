using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            var collideable = other.gameObject.GetComponent<IPlayerCollideable>();
            if (collideable != null)
            {
                collideable.OnPlayerCollision(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var collideable = other.gameObject.GetComponent<IPlayerCollideable>();
            if (collideable != null)
            {
                collideable.OnPlayerCollision(gameObject);
            }
        }
    }
}