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

        private bool _checked;

        public void HandleClick()
        {
            _checked = !_checked;

            if (_checked)
            {
                Debug.Log("HandleLevelCall");
                auctionCallController.HandleLevelCall(level);
            }
        }
    }
}