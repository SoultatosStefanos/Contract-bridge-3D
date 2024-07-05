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

        private void Awake()
        {
            _turnText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<AuctionTurnChangeEvent>(HandleTurnChangedEvent);

            if (_session.Auction?.Turn is { } turn)
            {
                UpdateVisual(turn);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleTurnChangedEvent);
        }

        private void HandleTurnChangedEvent(AuctionTurnChangeEvent e)
        {
            UpdateVisual(e.Seat);
        }

        private void UpdateVisual(Seat turnSeat)
        {
            _turnText.text = $"Turn: {turnSeat}";
        }
    }
}