using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class GameTurnPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private TextMeshProUGUI _turnText;

        private void Awake()
        {
            _turnText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<GameTurnChangeEvent>(HandleGameTurnChangeEvent);

            if (_session.Game?.Turn is { } turn)
            {
                UpdateVisual(turn);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<GameTurnChangeEvent>(HandleGameTurnChangeEvent);
        }

        private void HandleGameTurnChangeEvent(GameTurnChangeEvent e)
        {
            UpdateVisual(e.Seat);
        }

        private void UpdateVisual(Seat turnSeat)
        {
            _turnText.text = $"Turn: {turnSeat}";
        }
    }
}