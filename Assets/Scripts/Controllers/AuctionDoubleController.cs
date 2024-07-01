using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Controllers
{
    public class AuctionDoubleController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private ISession _session;

        public void HandleClick()
        {
            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            _session.Auction.Double(playerSeat);
        }
    }
}