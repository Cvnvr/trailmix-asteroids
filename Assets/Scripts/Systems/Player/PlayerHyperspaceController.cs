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
        
        private bool isInHyperspace;
        
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
            isInHyperspace = false;
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
            if (isInHyperspace)
                return;
            
            StartCoroutine(InitiateHyperspaceTravel());
        }

        private IEnumerator InitiateHyperspaceTravel()
        {
            isInHyperspace = true;
            
            TogglePlayerView(false);

            MoveToRandomLocationWithinBounds();
            yield return new WaitForSeconds(hyperspaceData.Duration);
            
            TogglePlayerView(true);
            
            isOnCooldown = true;
            isInHyperspace = false;
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