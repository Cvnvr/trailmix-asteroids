using TMPro;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [Inject] private SignalBus signalBus;

        private uint currentScore;
        
        [Inject]
        private void OnInject()
        {
            signalBus.Subscribe<ScoreAwardedEvent>(OnScoreAwarded);
        }

        private void Start()
        {
            // Init the score value and UI
            OnScoreAwarded(new ScoreAwardedEvent()
            {
                Score = 0
            });
        }

        private void OnScoreAwarded(ScoreAwardedEvent evt)
        {
            currentScore += evt.Score;
            scoreText.text = currentScore.ToString();
        }
    }
}