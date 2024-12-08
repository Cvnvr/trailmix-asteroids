namespace Asteroids
{
    public class WeaponBehaviourFactory : IWeaponBehaviourFactory
    {
        public IWeaponBehaviour GetBoundComponent(WeaponSpawnComponent weaponSpawnComponent, WeaponBehaviourData weaponBehaviourData)
        {
            switch (weaponBehaviourData)
            {
                case WeaponSpawnAdditionalData spawnAdditionalData:
                    var spawnAdditionalComponent = new WeaponSpawnAdditionalComponent();
                    spawnAdditionalComponent.Setup(
                        weaponSpawnComponent,
                        spawnAdditionalData.AngleOffset, 
                        spawnAdditionalData.SpawnDelay);
                    return spawnAdditionalComponent;
                default:
                    return null;
            }
        }
    }
}