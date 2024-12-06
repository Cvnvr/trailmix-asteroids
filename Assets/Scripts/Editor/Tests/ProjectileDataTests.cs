using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class ProjectileDataTests
    {
        [Test]
        public void Validate_PrefabIsSet()
        {
            var data = GetProjectileData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(ProjectileData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var projectile in data)
            {
                if (projectile.ProjectilePrefab == null)
                {
                    Debug.LogError(projectile.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(ProjectileData)} objects have a missing 'ProjectilePrefab' reference!");
        }
        
        [Test]
        public void Validate_BehavioursAreValid()
        {
            var data = GetProjectileData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(ProjectileData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var projectile in data)
            {
                if (projectile.Behaviours == null || projectile.Behaviours.Length == 0)
                    continue;

                foreach (var behaviour in projectile.Behaviours)
                {
                    if (behaviour == null)
                    {
                        Debug.LogError($"{projectile.name} - Behaviour is null.");
                        isValid = false;
                    }
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(ProjectileData)} objects have an invalid 'Behaviour' reference!");
        }
        
        private ProjectileData[] GetProjectileData()
        {
            var projectileData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<ProjectileData>(AssetPaths.ProjectileDataPath);
            return projectileData == null || projectileData.Length == 0 ? null : projectileData;
        }
    }
}
