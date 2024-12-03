namespace Asteroids
{
    public interface IProjectileBehaviourFactory
    {
        BaseProjectileBehaviourComponent GetBoundComponent(Projectile projectile, ProjectileBehaviourData projectileBehaviourData);
    }
}