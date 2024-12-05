using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class PlayerHyperspaceController : MonoBehaviour
    {
        [SerializeField] private PlayerHyperspaceData hyperspaceData;

        [Inject] private ScreenBoundsCalculator screenBoundsCalculator;
        [Inject] private SignalBus signalBus;

        private SpriteRenderer spriteRenderer;
        private Collider2D collider2d;

        private bool isOnCooldown;
        private float cooldown = 2f;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<HyperspaceInputEvent>(OnHyperspaceInput);
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2d = GetComponent<Collider2D>();
        }

        private void Start()
        {
            cooldown = hyperspaceData.Cooldown;
        }

        private void Update()
        {
            if (!isOnCooldown)
                return;
            
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                isOnCooldown = false;
                cooldown = hyperspaceData.Cooldown;
            }
        }

        private void OnHyperspaceInput(HyperspaceInputEvent evt)
        {
            StartCoroutine(InitiateHyperspaceTravel());
        }

        private IEnumerator InitiateHyperspaceTravel()
        {
            TogglePlayerView(false);

            MoveToRandomLocationWithinBounds();
            yield return new WaitForSeconds(hyperspaceData.Duration);
            
            TogglePlayerView(true);
            isOnCooldown = true;
        }
        
        private void TogglePlayerView(bool value)
        {
            spriteRenderer.enabled = value;
            collider2d.enabled = value;
        }
        
        private void MoveToRandomLocationWithinBounds()
        {
            var xPos = Random.Range(screenBoundsCalculator.LeftSide, screenBoundsCalculator.RightSide);
            var yPos = Random.Range(screenBoundsCalculator.BottomSide, screenBoundsCalculator.TopSide);

            transform.position = new Vector3(xPos, yPos, transform.position.z);
        }
        
        private void OnDisable()
        {
            signalBus.TryUnsubscribe<HyperspaceInputEvent>(OnHyperspaceInput);
        }
    }
}