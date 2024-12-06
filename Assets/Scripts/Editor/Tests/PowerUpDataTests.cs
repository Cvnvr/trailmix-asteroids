using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class PowerUpDataTests
    {
        [Test]
        public void Validate_WeaponDataIsSet()
        {
            var data = GetPowerUpData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PowerUpData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var powerUp in data)
            {
                if (powerUp.WeaponData == null)
                {
                    Debug.LogError(powerUp.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PowerUpData)} objects have a missing 'WeaponData' reference!");
        }
        
        [Test]
        public void Validate_SpriteIsSet()
        {
            var data = GetPowerUpData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PowerUpData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var powerUp in data)
            {
                if (powerUp.Sprite == null)
                {
                    Debug.LogError(powerUp.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PowerUpData)} objects have a missing 'Sprite' reference!");
        }
        
        [Test]
        public void Validate_TimerIsValid()
        {
            var data = GetPowerUpData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PowerUpData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var powerUp in data)
            {
                if (powerUp.TimerEnabled && powerUp.Timer <= 0f)
                {
                    Debug.LogError(powerUp.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PowerUpData)} objects have an invalid 'Timer' value set!");
        }
        
        private PowerUpData[] GetPowerUpData()
        {
            var powerUpData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<PowerUpData>(AssetPaths.PowerUpDataPath);
            return powerUpData == null || powerUpData.Length == 0 ? null : powerUpData;
        }
    }
}
