namespace Asteroids
{
    public interface IProjectileBehaviourFactory
    {
        IProjectileBehaviour GetBoundComponent(System.Action pushCallback, ProjectileBehaviourData projectileBehaviourData);
    }
}