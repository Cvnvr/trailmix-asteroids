using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/PlayerData", order = 1, fileName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private PlayerLifeData lifeData;
        [SerializeField] private PlayerMovementData movementData;
        [SerializeField] private PlayerHyperspaceData hyperspaceData;
        
        public GameObject Prefab => prefab;
        public PlayerLifeData LifeData => lifeData;
        public PlayerMovementData MovementData => movementData;
        public PlayerHyperspaceData HyperspaceData => hyperspaceData;
    }
}