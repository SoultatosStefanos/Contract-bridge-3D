using System;
using System.Collections.Generic;
using System.Linq;
using ContractBridge.Core;
using ContractBridge.Core.Impl;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Wrappers
{
    public class AuctionCallByLevelWrapper : AuctionActionWrapper
    {
        [FormerlySerializedAs("Bid Level")]
        [SerializeField]
        private Level bidLevel;

        [Inject]
        private IBidFactory _bidFactory;

        public override bool CanPlayAction()
        {
            return AllDenominations().Any(denomination => Auction.CanCall(
                    _bidFactory.Create(bidLevel, denomination), PlayerSeat
                )
            );
        }

        private static IEnumerable<Denomination> AllDenominations()
        {
            return Enum.GetValues(typeof(Denomination)).Cast<Denomination>();
        }
    }
}