using System;
using UnityEngine;

namespace Asteroids
{
    public class ProjectileDestroyOtherAfterCollisionComponent : IProjectileBehaviour
    {
        public void Init(Action pushCallback)
        {
        }

        public void Update()
        {
        }

        public void OnCollision(GameObject collision)
        {
            if (collision.GetComponent<IDestructible>() != null)
            {
                collision.GetComponent<IDestructible>().Destroy();
            }
        }
    }
}