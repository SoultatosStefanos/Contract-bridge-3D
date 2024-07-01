using Buttons;
using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    [RequireComponent(typeof(ToggleButton))]
    public class AuctionCallLevelController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [FormerlySerializedAs("Level")]
        [SerializeField]
        private Level level;

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
            auctionCallController.HandleLevelCall(_toggleButton.Checked ? level : null);
        }
    }
}