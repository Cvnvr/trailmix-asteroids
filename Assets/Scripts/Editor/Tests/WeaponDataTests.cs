using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class WeaponDataTests
    {
        [Test]
        public void Validate_ProjectileDataIsSet()
        {
            var data = GetWeaponData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(WeaponData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var weapon in data)
            {
                if (weapon.ProjectileData == null)
                {
                    Debug.LogError(weapon.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(WeaponData)} objects are missing a 'ProjectileData' reference!");
        }
        
        [Test]
        public void Validate_SpeedIsValid()
        {
            var data = GetWeaponData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(WeaponData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var weapon in data)
            {
                if (weapon.Speed <= 0)
                {
                    Debug.LogError(weapon.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(WeaponData)} objects have an invalid 'Speed' value set!");
        }
        
        [Test]
        public void Validate_DelayIsValid()
        {
            var data = GetWeaponData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(WeaponData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var weapon in data)
            {
                if (weapon.Delay < 0)
                {
                    Debug.LogError(weapon.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(WeaponData)} objects have an invalid 'Delay' value set!");
        }
        
        [Test]
        public void Validate_BehavioursAreValid()
        {
            var data = GetWeaponData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(WeaponData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var weapon in data)
            {
                if (weapon.BehaviourData == null || weapon.BehaviourData.Length == 0)
                    continue;

                foreach (var behaviour in weapon.BehaviourData)
                {
                    if (behaviour == null)
                    {
                        Debug.LogError($"{weapon.name} - BehaviourData is null.");
                        isValid = false;
                    }
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(WeaponData)} objects have an invalid 'BehaviourData' reference!");
        }
        
        private WeaponData[] GetWeaponData()
        {
            var weaponData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<WeaponData>(AssetPaths.WeaponDataPath);
            return weaponData == null || weaponData.Length == 0 ? null : weaponData;
        }
    }
}
