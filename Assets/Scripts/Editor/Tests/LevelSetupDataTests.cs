using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class LevelSetupDataTests
    {
        [Test]
        public void Validate_AsteroidToSpawnIsSet()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.AsteroidToSpawn == null)
                {
                    Debug.LogError(data.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects are missing a 'AsteroidToSpawn' reference!");
        }
        
        [Test]
        public void Validate_AsteroidsInitialSpawnCountIsSet()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.AsteroidsInitialSpawnCount == 0)
                {
                    Debug.LogError($"{data.name} - AsteroidsInitialSpawnCount is set to 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'AsteroidsInitialSpawnCount' value set!");
        }
        
        [Test]
        public void Validate_AsteroidsMaxSpawnCountIsValid()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.AsteroidsMaxSpawnCount == 0)
                    continue;
                
                if (data.AsteroidsMaxSpawnCount < data.AsteroidsInitialSpawnCount)
                {
                    Debug.LogError($"{data.name} - AsteroidsMaxSpawnCount is less than AsteroidsInitialSpawnCount.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'AsteroidsMaxSpawnCount' value set!");
        }
        
        [Test]
        public void Validate_TimeBetweenWavesIsValid()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.TimeBetweenWaves < 0)
                {
                    Debug.LogError($"{data.name} - TimeBetweenWaves is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'TimeBetweenWaves' value set!");
        }
        
        [Test]
        public void Validate_UfoChanceToSpawnIsValid()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.UfoChanceToSpawn < 0)
                {
                    Debug.LogError($"{data.name} - UfoChanceToSpawn is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'UfoChanceToSpawn' value set!");
        }
        
        [Test]
        public void Validate_UfoSpawnCheckTimeDelayIsValid()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.UfoSpawnCheckTimeDelay < 0)
                {
                    Debug.LogError($"{data.name} - UfoSpawnCheckTimeDelay is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'UfoSpawnCheckTimeDelay' value set!");
        }
        
        [Test]
        public void Validate_SpawnDirectionToleranceIsValid()
        {
            var levelSetupData = GetLevelSetupData();
            if (levelSetupData == null)
            {
                Assert.Pass($"No {nameof(LevelSetupData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var data in levelSetupData)
            {
                if (data.SpawnDirectionTolerance < 0)
                {
                    Debug.LogError($"{data.name} - SpawnDirectionTolerance is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'SpawnDirectionTolerance' value set!");
        }
        
        private LevelSetupData[] GetLevelSetupData()
        {
            var levelSetupData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<LevelSetupData>(AssetPaths.LevelSetupDataPath);
            return levelSetupData == null || levelSetupData.Length == 0 ? null : levelSetupData;
        }
    }
}
