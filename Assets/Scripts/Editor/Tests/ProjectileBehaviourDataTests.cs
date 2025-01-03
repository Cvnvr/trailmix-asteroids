using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class ProjectileBehaviourDataTests
    {
        [Test]
        public void Validate_ProjectileBehavioursAreValid()
        {
            var data = GetProjectileBehaviourData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(ProjectileBehaviourData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var projectileBehaviour in data)
            {
                switch (projectileBehaviour)
                {
                    case ProjectileDestroySelfAfterTimeData destroySelfAfterTime:
                        if (destroySelfAfterTime.Lifetime <= 0)
                        {
                            Debug.LogError($"{projectileBehaviour.name} - Lifetime value is less than or equal to 0.");
                            isValid = false;
                        }
                        break;
                    case ProjectileDestroySelfAfterDistanceData destroySelfAfterDistance:
                        if (destroySelfAfterDistance.Distance <= 0)
                        {
                            Debug.LogError($"{projectileBehaviour.name} - Distance value is less than or equal to 0.");
                            isValid = false;
                        }
                        break;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(ProjectileBehaviourData)} objects have invalid setups!");
        }
        
        private ProjectileBehaviourData[] GetProjectileBehaviourData()
        {
            var projectileBehaviourData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<ProjectileBehaviourData>(AssetPaths.ProjectileDataPath);
            return projectileBehaviourData == null || projectileBehaviourData.Length == 0 ? null : projectileBehaviourData;
        }
    }
}
