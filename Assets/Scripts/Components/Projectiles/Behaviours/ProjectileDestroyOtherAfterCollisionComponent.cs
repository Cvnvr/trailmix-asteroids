using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroyOtherAfterCollisionComponent : IProjectileBehaviour
    {
        private string[] tagsToIgnore;
        
        public void Setup(Action pushCallback, string[] tagsToIgnore)
        {
            Init(pushCallback);
            this.tagsToIgnore = tagsToIgnore;
        }
        
        public void Init(Action pushCallback)
        {
        }

        public void Update()
        {
        }

        public void OnCollision(GameObject collision)
        {
            if (tagsToIgnore is { Length: > 0 })
            {
                foreach (var tag in tagsToIgnore)
                {
                    if (collision.CompareTag(tag))
                        return;
                }
            }
            
            if (collision.GetComponent<IDestructible>() != null)
            {
                collision.GetComponent<IDestructible>().Destroy();
            }
        }
    }
}