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

        private void Awake()
        {
            _dealerText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (_board.Dealer is { } dealer)
            {
                UpdateVisual(dealer);
            }
        }

        private void UpdateVisual(Seat dealerSeat)
        {
            _dealerText.text = $"Dealer: {dealerSeat}";
        }
    }
}