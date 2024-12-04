namespace Asteroids
{
    public class WeaponBehaviourFactory : IWeaponBehaviourFactory
    {
        public IWeaponBehaviour GetBoundComponent(WeaponSpawnData spawnData, WeaponBehaviourData weaponBehaviourData)
        {
            switch (weaponBehaviourData)
            {
                case WeaponSpawnAdditionalData spawnAdditionalData:
                    var spawnAdditionalComponent = new WeaponSpawnAdditionalComponent();
                    spawnAdditionalComponent.Setup(
                        spawnData,
                        spawnAdditionalData.AngleOffset, 
                        spawnAdditionalData.SpawnDelay);
                    return spawnAdditionalComponent;
                default:
                    return null;
            }
        }
    }
}