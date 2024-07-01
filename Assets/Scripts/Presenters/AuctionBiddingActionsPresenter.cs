using System.Collections.Generic;
using System.Linq;
using ContractBridge.Core;
using Events;
using UnityEngine;
using UnityEngine.Serialization;
using Wrappers;
using Zenject;

namespace Presenters
{
    public class AuctionBiddingActionsPresenter : MonoBehaviour
    {
        private const string AuctionActionButtonTag = "Auction Action Button";

        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        private IEnumerable<GameObject> _auctionActionButtons;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _eventBus.On<AuctionTurnChangeEvent>(HandleAuctionTurnChangedEvent);
            _eventBus.On<AuctionFinalContractEvent>(HandleAuctionTurnFinalContractEvent);
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionTurnChangeEvent>(HandleAuctionTurnChangedEvent);
            _eventBus.Off<AuctionFinalContractEvent>(HandleAuctionTurnFinalContractEvent);
        }

        private void HandleAuctionTurnChangedEvent(AuctionTurnChangeEvent obj)
        {
            UpdateAuctionActionButtons();
        }

        private void HandleAuctionTurnFinalContractEvent(AuctionFinalContractEvent obj)
        {
            UpdateAuctionActionButtons();
        }

        private void UpdateAuctionActionButtons()
        {
            foreach (var actionButton in FindAuctionActionButtons())
            {
                var actionWrapper = actionButton.GetComponent<AuctionActionWrapper>();
                actionButton.SetActive(actionWrapper.CanPlayAction());
            }
        }

        private IEnumerable<GameObject> FindAuctionActionButtons()
        {
            if (_auctionActionButtons != null)
            {
                return _auctionActionButtons;
            }

            var children = new List<GameObject>();
            GetChildrenInHierarchy(transform, children);
            _auctionActionButtons = children.Where(child => child.CompareTag(AuctionActionButtonTag));
            return _auctionActionButtons;
        }

        private static void GetChildrenInHierarchy(Transform parent, ICollection<GameObject> result)
        {
            foreach (Transform child in parent)
            {
                result.Add(child.gameObject);
                GetChildrenInHierarchy(child, result);
            }
        }
    }
}