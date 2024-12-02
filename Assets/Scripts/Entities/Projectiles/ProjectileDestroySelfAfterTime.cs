using System.Collections;
using UnityEngine;

namespace Entities.Projectiles
{
    public class ProjectileDestroySelfAfterTime : BaseProjectileBehaviour
    {
        [SerializeField] private float lifetime;

        private Coroutine destroySelfCoroutine;

        public override void Init(Projectile projectile)
        {
            base.Init(projectile);
            
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