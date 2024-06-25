using ContractBridge.Core;
using Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class AuctionTurnPresenter : MonoBehaviour
    {
        [Inject]
        private IEventBus _eventBus;

        private TextMeshProUGUI _turnText;

        private void OnEnable()
        {
            _turnText = GetComponent<TextMeshProUGUI>();

            _eventBus.On<AuctionTurnChangeEvent>(HandleTurnChangedEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleTurnChangedEvent);
        }

        private void HandleTurnChangedEvent(AuctionTurnChangeEvent e)
        {
            UpdateTurn(e.Seat);
        }

        private void UpdateTurn(Seat turnSeat)
        {
            _turnText.text = $"Turn: {turnSeat}";
        }
    }
}