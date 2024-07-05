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

        [Inject]
        private ISession _session;

        private TextMeshProUGUI _turnText;

        private void OnEnable()
        {
            _turnText = GetComponent<TextMeshProUGUI>();

            _eventBus.On<AuctionTurnChangeEvent>(HandleTurnChangedEvent);
            
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            Debug.Assert(_session.Auction.Turn != null, "_session.Auction.Turn != null");
            UpdateTurn((Seat)_session.Auction.Turn);
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