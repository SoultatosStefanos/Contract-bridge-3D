using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Presenters
{
    public class AuctionPassPresenter : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Pass Button")]
        [SerializeField]
        private GameObject passButton;

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
            passButton.SetActive(_session.Auction.CanPass(playerSeat));
        }
    }
}