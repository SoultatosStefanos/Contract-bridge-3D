using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class DealerSetupPresenter : MonoBehaviour
    {
        [Inject]
        private IBoard _board;

        private TextMeshProUGUI _dealerText;

        [Inject]
        private IEventBus _eventBus;

        private void Start()
        {
            _dealerText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<BoardDealerSetEvent>(HandleDealerSetEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<BoardDealerSetEvent>(HandleDealerSetEvent);
        }

        private void HandleDealerSetEvent(BoardDealerSetEvent evt)
        {
            UpdateDealerText(evt.Dealer);
        }

        private void UpdateDealerText(Seat dealerSeat)
        {
            _dealerText.text = $"Dealer: {dealerSeat}";
        }
    }
}