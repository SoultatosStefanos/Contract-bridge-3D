using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    public class ScorePresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Partnership")]
        [SerializeField]
        private Partnership partnership;

        [Inject]
        private IEventBus _eventBus;

        private TextMeshProUGUI _scoreText;

        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<PairScoreEvent>(HandlePairScoreEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<PairScoreEvent>(HandlePairScoreEvent);
        }

        private void HandlePairScoreEvent(PairScoreEvent e)
        {
            var eventPair = e.Pair;
            var eventPartnership = eventPair.Partnership;

            if (eventPartnership != partnership)
            {
                return;
            }

            UpdateScore(eventPair.Score);
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}