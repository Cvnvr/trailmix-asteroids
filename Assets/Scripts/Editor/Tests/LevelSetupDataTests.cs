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
        public void Validate_InitialNumberToSpawnIsSet()
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
                if (data.InitialNumberToSpawn == 0)
                {
                    Debug.LogError($"{data.name} - InitialNumberToSpawn is set to 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'InitialNumberToSpawn' value set!");
        }
        
        [Test]
        public void Validate_MaxNumberToSpawnIsValid()
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
                if (data.MaxNumberToSpawn == 0)
                    continue;
                
                if (data.MaxNumberToSpawn < data.InitialNumberToSpawn)
                {
                    Debug.LogError($"{data.name} - MaxNumberToSpawn is less than InitialNumberToSpawn.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'MaxNumberToSpawn' value set!");
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
        public void Validate_ChanceToSpawnUfoIsValid()
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
                if (data.ChanceToSpawnUfo < 0)
                {
                    Debug.LogError($"{data.name} - ChanceToSpawnUfo is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'ChanceToSpawnUfo' value set!");
        }
        
        [Test]
        public void Validate_TimeBetweenUfoSpawnsIsValid()
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
                if (data.TimeBetweenUfoSpawnChecks < 0)
                {
                    Debug.LogError($"{data.name} - TimeBetweenUfoSpawnChecks is less than 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(LevelSetupData)} objects have an invalid 'TimeBetweenUfoSpawnChecks' value set!");
        }
        
        private LevelSetupData[] GetLevelSetupData()
        {
            var levelSetupData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<LevelSetupData>(AssetPaths.LevelSetupDataPath);
            return levelSetupData == null || levelSetupData.Length == 0 ? null : levelSetupData;
        }
    }
}
