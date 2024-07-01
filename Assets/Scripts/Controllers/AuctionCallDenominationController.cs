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

        private bool _checked;

        public void HandleClick()
        {
            _checked = !_checked;

            if (_checked)
            {
                Debug.Log("HandleDenominationCall");
                auctionCallController.HandleDenominationCall(denomination);
            }
        }
    }
}