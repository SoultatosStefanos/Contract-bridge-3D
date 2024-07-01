using Buttons;
using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    [RequireComponent(typeof(ToggleButton))]
    public class AuctionCallDenominationController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Denomination")]
        [SerializeField]
        private Denomination denomination;

        [FormerlySerializedAs("Auction Call Controller")]
        [SerializeField]
        private AuctionCallController auctionCallController;

        private ToggleButton _toggleButton;

        private void Start()
        {
            _toggleButton = GetComponent<ToggleButton>();
        }

        public void HandleToggle()
        {
            auctionCallController.HandleDenominationCall(_toggleButton.Checked ? denomination : null);
        }
    }
}