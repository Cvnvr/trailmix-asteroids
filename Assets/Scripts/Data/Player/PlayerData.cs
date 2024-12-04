using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/Player/PlayerData", order = 1, fileName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerMovementData movementData;
        [SerializeField] private Sprite playerSprite;
        
        public PlayerMovementData MovementData => movementData;
        public Sprite PlayerSprite => playerSprite;
    }
}