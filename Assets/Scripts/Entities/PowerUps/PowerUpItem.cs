using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class PowerUpItem : MonoBehaviour, IPlayerCollideable
    { 
        [Inject] private SignalBus signalBus;

        private SpriteRenderer spriteRenderer;

        private WeaponData powerUp;
        private bool isTimerEnabled;
        private float timer;
        
        private bool isInitialised;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Init(PowerUpData data)
        {
            isInitialised = false;
            
            gameObject.name = data.PowerUpName;
            powerUp = data.WeaponData;
            spriteRenderer.sprite = data.Sprite;
            isTimerEnabled = data.TimerEnabled;
            timer = data.Timer;

            isInitialised = true;
        }

        private void Update()
        {
            if (!isInitialised)
                return;
            
            if (!isTimerEnabled)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void OnPlayerCollision(GameObject player)
        {
            if (!isInitialised)
                return;

            signalBus.TryFire(new PowerUpCollectedEvent() { PowerUp = powerUp });
            Destroy(gameObject);
        }
    }
}