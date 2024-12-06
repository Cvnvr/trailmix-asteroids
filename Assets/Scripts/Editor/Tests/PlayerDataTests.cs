using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class PlayerDataTests
    {
        [Test]
        public void Validate_PrefabIsSet()
        {
            var data = GetPlayerData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PlayerData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var player in data)
            {
                if (player.Prefab == null)
                {
                    Debug.LogError(player.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PlayerData)} objects are missing a 'Prefab' reference!");
        }
        
        [Test]
        public void Validate_FieldsAreSet()
        {
            var data = GetPlayerData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(PlayerData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var player in data)
            {
                if (player.LifeData == null)
                {
                    Debug.LogError($"{player.name} - LifeData is not set.");
                    isValid = false;
                }
                
                if (player.MovementData == null)
                {
                    Debug.LogError($"{player.name} - MovementData is not set.");
                    isValid = false;
                }
                
                if (player.HyperspaceData == null)
                {
                    Debug.LogError($"{player.name} - HyperspaceData is not set.");
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(PlayerData)} objects are missing data reference(s)!");
        }
        
        private PlayerData[] GetPlayerData()
        {
            var playerData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<PlayerData>(AssetPaths.PlayerDataPath);
            return playerData == null || playerData.Length == 0 ? null : playerData;
        }
    }
}
