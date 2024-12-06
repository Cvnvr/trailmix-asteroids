using UnityEditor;
using UnityEngine;

namespace Asteroids.Editor
{
    public static class ScriptableObjectFinder
    {
#if UNITY_EDITOR
        public static T[] GetScriptableObjectsOfTypeInFolder<T>(string folderPath) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folderPath });
            var results = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset == null)
                {
                    Debug.LogWarning($"[{nameof(ScriptableObjectFinder)}.{nameof(GetScriptableObjectsOfTypeInFolder)}] Failed to load asset at path: {path}");
                    continue;
                }
                results[i] = asset;
            }

            return results;
        }
#endif
    }
}