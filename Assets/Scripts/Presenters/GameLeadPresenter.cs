using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Presenters
{
    public class GameLeadPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        private TextMeshProUGUI _leadText;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _leadText = GetComponent<TextMeshProUGUI>();

            _eventBus.On<GameLeadChangeEvent>(HandleGameLeadChangeEvent);

            Debug.Assert(_session.Game != null, "_session.Game != null");
            Debug.Assert(_session.Game.Lead != null, "_session.Game.Lead != null");
            UpdateLead((Seat)_session.Game.Lead);
        }

        private void OnDisable()
        {
            _eventBus.On<GameLeadChangeEvent>(HandleGameLeadChangeEvent);
        }

        private void HandleGameLeadChangeEvent(GameLeadChangeEvent e)
        {
            UpdateLead(e.Seat);
        }

        private void UpdateLead(Seat leadSeat)
        {
            _leadText.text = $"Lead: {leadSeat}";
        }
    }
}