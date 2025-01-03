namespace Asteroids
{
    public interface IProjectileBehaviourFactory
    {
        IProjectileBehaviour GetBoundComponent(Projectile projectile, System.Action pushCallback, ProjectileBehaviourData projectileBehaviourData);
    }
}