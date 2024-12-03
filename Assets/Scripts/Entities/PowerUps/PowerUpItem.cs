using UnityEngine;
using Zenject;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class PowerUpItem : MonoBehaviour
    { 
        [Inject] private SignalBus signalBus;

        private SpriteRenderer renderer;

        private WeaponData powerUp;
        private float timer;

        private bool isTimerEnabled;
        private bool isInitialised;
        
        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
        }
        
        public void Init(PowerUpData data)
        {
            isInitialised = false;
            
            gameObject.name = data.PowerUpName;
            powerUp = data.WeaponData;
            renderer.sprite = data.Sprite;
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            signalBus.TryFire(new PowerUpCollectedEvent() { PowerUp = powerUp });
            Destroy(gameObject);
        }
    }
}