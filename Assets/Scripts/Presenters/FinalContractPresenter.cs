using ContractBridge.Core;
using Events;
using Extensions;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class FinalContractPresenter : MonoBehaviour
    {
        private TextMeshProUGUI _contractText;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void Awake()
        {
            _contractText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _eventBus.On<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);

            if (_session.Auction is { } auction)
            {
                UpdateVisual(auction.FinalContract);
            }
        }

        private void OnDisable()
        {
            _eventBus.Off<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);
        }

        private void HandleAuctionFinalContractEvent(AuctionFinalContractEvent e)
        {
            UpdateVisual(e.Contract);
        }

        private void UpdateVisual(IContract contract)
        {
            _contractText.text = FormatContract(contract);
        }

        private static string FormatContract(IContract contract)
        {
            var level = contract.Level.ToNumeralString();
            var denomination = contract.Denomination.ToShortHandString();
            var declarer = contract.Declarer.ToShortHandString();
            return $"Contract: {level} {denomination} ({declarer})";
        }
    }
}