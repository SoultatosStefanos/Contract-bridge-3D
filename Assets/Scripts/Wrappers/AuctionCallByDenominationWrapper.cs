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
    public class AuctionCallByDenominationWrapper : AuctionActionWrapper
    {
        [FormerlySerializedAs("Bid Denomination")]
        [SerializeField]
        private Denomination denomination;

        [Inject]
        private IBidFactory _bidFactory;

        public override bool CanPlayAction()
        {
            return AllLevels().Any(level => Auction.CanCall(_bidFactory.Create(level, denomination), PlayerSeat));
        }

        private static IEnumerable<Level> AllLevels()
        {
            return Enum.GetValues(typeof(Level)).Cast<Level>();
        }
    }
}