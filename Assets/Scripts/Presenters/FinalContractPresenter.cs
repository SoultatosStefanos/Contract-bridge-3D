using ContractBridge.Core;
using Events;
using Extensions;
using TMPro;
using UnityEngine;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Presenters
{
    public class FinalContractPresenter : MonoBehaviour
    {
        private TextMeshProUGUI _contractText;

        [Inject]
        private IEventBus _eventBus;

        [Inject]
        private ISession _session;

        private void OnEnable()
        {
            _contractText = GetComponent<TextMeshProUGUI>();

            _eventBus.On<AuctionFinalContractEvent>(HandleAuctionFinalContractEvent);

            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            UpdateVisual(_session.Auction.FinalContract);
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
            var partnership = contract.Declarer.Partnership().ToShortHandString();
            return $"Contract: {level} {denomination} ({partnership})";
        }
    }
}