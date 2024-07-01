using ContractBridge.Core;
using ContractBridge.Core.Impl;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Debug = System.Diagnostics.Debug;

namespace Controllers
{
    public class AuctionCallController : MonoBehaviour
    {
        [FormerlySerializedAs("Player Seat")]
        [SerializeField]
        private Seat playerSeat;

        [Inject]
        private IBidFactory _bidFactory;

        private Denomination? _denomination;

        private Level? _level;

        [Inject]
        private ISession _session;

        public void HandleLevelCall(Level? level)
        {
            _level = level;
            CallIfPlayIsSet();
        }

        public void HandleDenominationCall(Denomination? denomination)
        {
            _denomination = denomination;
            CallIfPlayIsSet();
        }

        private void CallIfPlayIsSet()
        {
            if (_level is not { } level || _denomination is not { } denomination)
            {
                return;
            }

            Debug.Assert(_session.Auction != null, "_session.Auction != null");
            _session.Auction.Call(_bidFactory.Create(level, denomination), playerSeat);

            ResetCallValues(); // TODO(?) Remove? Probably doesn't matter.
        }

        private void ResetCallValues()
        {
            _level = null;
            _denomination = null;
        }
    }
}