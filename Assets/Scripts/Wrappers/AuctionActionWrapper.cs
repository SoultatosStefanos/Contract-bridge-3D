using ContractBridge.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Wrappers
{
    public abstract class AuctionActionWrapper : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private ISession _session;

        protected Seat PlayerSeat => playerSeat;

        protected IAuction Auction => _session.Auction;

        public abstract bool CanPlayAction();
    }
}