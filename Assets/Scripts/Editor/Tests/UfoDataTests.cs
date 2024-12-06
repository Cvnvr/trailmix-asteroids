using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class UfoDataTests
    {
        [Test]
        public void Validate_PrefabIsSet()
        {
            var data = GetUfoData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(UfoData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var ufo in data)
            {
                if (ufo.Prefab == null)
                {
                    Debug.LogError(ufo.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(UfoData)} objects are missing a 'Prefab' reference!");
        }

        [Test]
        public void Validate_TimeBetweenShotsIsValid()
        {
            var data = GetUfoData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(UfoData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var ufo in data)
            {
                if (ufo.TimeBetweenShots <= 0)
                {
                    Debug.LogError(ufo.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(UfoData)} objects have an invalid 'TimeBetweenShots' value!");
        }
        
        [Test]
        public void Validate_ChangeDirectionIntervalIsValid()
        {
            var data = GetUfoData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(UfoData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var ufo in data)
            {
                if (ufo.ChangeDirectionInterval <= 0)
                {
                    Debug.LogError(ufo.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(UfoData)} objects have an invalid 'ChangeDirectionInterval' value!");
        }
        
        private UfoData[] GetUfoData()
        {
            var ufoData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<UfoData>(AssetPaths.UfoDataPath);
            return ufoData == null || ufoData.Length == 0 ? null : ufoData;
        }
    }
}
