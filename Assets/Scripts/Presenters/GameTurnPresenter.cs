using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Presenters
{
    public class GameTurnPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;
        
        private TextMeshProUGUI _turnText;

        private void OnEnable()
        {
            _turnText = GetComponent<TextMeshProUGUI>();

            _eventBus.On<GameTurnChangeEvent>(HandleGameTurnChangeEvent);

            Debug.Assert(_session.Game != null, "_session.Game != null");
            Debug.Assert(_session.Game.Turn != null, "_session.Game.Turn != null");
            UpdateTurn((Seat)_session.Game.Turn);
        }

        private void OnDisable()
        {
            _eventBus.Off<GameTurnChangeEvent>(HandleGameTurnChangeEvent);
        }

        private void HandleGameTurnChangeEvent(GameTurnChangeEvent e)
        {
            UpdateTurn(e.Seat);
        }

        private void UpdateTurn(Seat turnSeat)
        {
            _turnText.text = $"Turn: {turnSeat}";
        }
    }
}