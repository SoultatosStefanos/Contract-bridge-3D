using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class GameLeadPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        private TextMeshProUGUI _leadText;

        [Inject]
        private ISession _session;

        private void Awake()
        {
            _leadText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<GameLeadChangeEvent>(HandleGameLeadChangeEvent);

            if (_session.Game?.Lead is { } lead)
            {
                UpdateVisual(lead);
            }
        }

        private void OnDisable()
        {
            _eventBus.On<GameLeadChangeEvent>(HandleGameLeadChangeEvent);
        }

        private void HandleGameLeadChangeEvent(GameLeadChangeEvent e)
        {
            UpdateVisual(e.Seat);
        }

        private void UpdateVisual(Seat leadSeat)
        {
            _leadText.text = $"Lead: {leadSeat}";
        }
    }
}