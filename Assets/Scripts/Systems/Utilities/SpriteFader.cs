using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 1f;

        private SpriteRenderer spriteRenderer;

        private float elapsedTime = 0f;
        private bool isFadingIn = false;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            var alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            var targetAlpha = isFadingIn ? alpha : 1 - alpha;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);

            if (alpha >= 1f)
            {
                isFadingIn = !isFadingIn;
                elapsedTime = 0f;
            }
        }
    }
}