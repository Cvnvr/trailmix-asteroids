namespace Asteroids
{
    public interface IWeaponBehaviourFactory
    {
        IWeaponBehaviour GetBoundComponent(WeaponSpawnComponent weaponSpawnComponent, WeaponBehaviourData weaponBehaviourData);
    }
}