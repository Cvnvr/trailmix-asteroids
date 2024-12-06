using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class PlayerMovementDataTests
    {
        [Test]
        public void Validate_ValuesAreValid()
        {
            var data = GetPlayerMovementData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PlayerMovementData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var player in data)
            {
                if (player.ForwardThrust <= 0)
                {
                    Debug.LogError($"{player.name} - ForwardThrust is less than or equal to 0.");
                    isValid = false;
                }
                
                if (player.MaxForwardSpeed <= 0)
                {
                    Debug.LogError($"{player.name} - MaxForwardSpeed is less than or equal to 0.");
                    isValid = false;
                }
                
                if (player.RotationalThrust <= 0)
                {
                    Debug.LogError($"{player.name} - RotationalThrust is less than or equal to 0.");
                    isValid = false;
                }
                
                if (player.MaxRotationalSpeed <= 0)
                {
                    Debug.LogError($"{player.name} - MaxRotationalSpeed is less than or equal to 0.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PlayerMovementData)} objects have invalid values set!");
        }
        
        private PlayerMovementData[] GetPlayerMovementData()
        {
            var playerMovementData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<PlayerMovementData>(AssetPaths.PlayerDataPath);
            return playerMovementData == null || playerMovementData.Length == 0 ? null : playerMovementData;
        }
    }
}
