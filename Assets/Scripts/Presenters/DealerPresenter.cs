using ContractBridge.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class DealerPresenter : MonoBehaviour
    {
        [Inject]
        private IBoard _board;

        private TextMeshProUGUI _dealerText;

        private void Start()
        {
            _dealerText = GetComponent<TextMeshProUGUI>();
            UpdateDealerTextFromBoard();
        }

        private void UpdateDealerTextFromBoard()
        {
            if (_board.Dealer is { } dealer)
            {
                UpdateDealerText(dealer);
            }
        }

        private void UpdateDealerText(Seat dealerSeat)
        {
            _dealerText.text = $"Dealer: {dealerSeat}";
        }
    }
}