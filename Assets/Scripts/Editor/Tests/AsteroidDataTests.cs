using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Editor.Tests
{
    public class AsteroidDataTests
    {
        [Test]
        public void Validate_PrefabIsSet()
        {
            var data = GetAsteroidData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(AsteroidData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var asteroid in data)
            {
                if (asteroid.Prefab == null)
                {
                    Debug.LogError(asteroid.name);
                    isValid = false;
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(AsteroidData)} objects are missing a 'Prefab' reference!");
        }
        
        [Test]
        public void Validate_SpawnedAsteroidDataIsSet()
        {
            var data = GetAsteroidData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(AsteroidData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var asteroid in data)
            {
                if (!asteroid.DoesSpawnMoreOnDestruction)
                    continue;

                if (asteroid.SpawnedAsteroidData != null)
                    continue;
                
                Debug.LogError(asteroid.name);
                isValid = false;
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(AsteroidData)} objects has 'DoesSpawnMoreOnDestruction' set to true but is missing a 'SpawnedAsteroidData' reference!");
        }
        
        [Test]
        public void Validate_NumberToSpawnIsSet()
        {
            var data = GetAsteroidData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(AsteroidData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var asteroid in data)
            {
                if (!asteroid.DoesSpawnMoreOnDestruction)
                    continue;

                if (asteroid.NumberToSpawn > 0)
                    continue;
                
                Debug.LogError(asteroid.name);
                isValid = false;
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(AsteroidData)} objects has 'DoesSpawnMoreOnDestruction' set to true, but 'NumberToSpawn' is set to 0!");
        }

        [Test]
        public void Validate_SpritesAreSet()
        {
            var data = GetAsteroidData();
            if (data == null || data.Length == 0)
            {
                Assert.Pass($"No {nameof(AsteroidData)} found.");
                return;
            }
            
            var isValid = true;
            foreach (var asteroid in data)
            {
                if (asteroid.Sprites == null || asteroid.Sprites.Length == 0)
                {
                    Debug.LogError($"{asteroid.name} - No Sprites set.");
                    isValid = false;
                    continue;
                }

                foreach (var sprite in asteroid.Sprites)
                {
                    if (sprite == null)
                    {
                        Debug.LogError($"{asteroid.name} - Empty Sprite reference found.");
                        isValid = false;
                    }
                }
            }
            
            Assert.IsTrue(isValid, $"The following {nameof(AsteroidData)} objects are missing Sprite references!");
        }
        
        private AsteroidData[] GetAsteroidData()
        {
            var asteroidData = ScriptableObjectFinder.GetScriptableObjectsOfTypeInFolder<AsteroidData>(AssetPaths.AsteroidsDataPath);
            return asteroidData == null || asteroidData.Length == 0 ? null : asteroidData;
        }
    }
}
