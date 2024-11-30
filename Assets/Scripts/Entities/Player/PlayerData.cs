using UnityEngine;

namespace Entities.Player
{
    [CreateAssetMenu(menuName = "Data/Entities/Player/Data", order = 1, fileName = "New Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerMovementData movementData;
        [SerializeField] private Sprite playerSprite;
        
        public PlayerMovementData MovementData => movementData;
        public Sprite PlayerSprite => playerSprite;
    }
}