using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Presenters
{
    public class AuctionDoublePresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Double Button")]
        [SerializeField]
        private GameObject doubleButton;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
            _eventBus.On<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleAuctionTurnChangeEvent);
            _eventBus.Off<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);
        }

        private void HandleAuctionTurnChangeEvent(AuctionTurnChangeEvent e)
        {
            UpdateVisual();
        }

        private void HandleAuctionFinalContractEvent(AuctionFinalContractEvent e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            doubleButton.SetActive(_session.Auction.CanDouble(playerSeat));
        }
    }
}