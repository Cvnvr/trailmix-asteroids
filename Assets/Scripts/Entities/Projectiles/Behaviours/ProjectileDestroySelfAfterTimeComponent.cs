using System.Collections;
using UnityEngine;

namespace Entities.Projectiles
{
    public class ProjectileDestroySelfAfterTimeComponent : BaseProjectileBehaviour
    {
        private Coroutine destroySelfCoroutine;

        public void Setup(Projectile projectile, float lifetime)
        {
            Init(projectile);
            destroySelfCoroutine = StartCoroutine(DestroySelfAfterDelay(lifetime));
        }

        private IEnumerator DestroySelfAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (projectile != null)
            {
                projectile.ReturnToPool();
            }
        }

        private void OnDisable()
        {
            if (destroySelfCoroutine != null)
            {
                StopCoroutine(destroySelfCoroutine);
                destroySelfCoroutine = null;
            }

            projectile = null;
        }
    }
}