using Makaretu.Bridge;
using Resolvers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenters
{
    public class DealerPresenter : MonoBehaviour
    {
        [Inject]
        private IBoardResolver _boardResolver;

        private TextMeshProUGUI _dealerText;

        private void Start()
        {
            _dealerText = GetComponent<TextMeshProUGUI>();

            UpdateDealerTextFromBoard();
        }

        private void UpdateDealerTextFromBoard()
        {
            var board = _boardResolver.GetBoard();
            UpdateDealerText(board.Dealer);
        }

        private void UpdateDealerText(Seat dealerSeat)
        {
            _dealerText.text = $"Dealer: {dealerSeat}";
        }
    }
}