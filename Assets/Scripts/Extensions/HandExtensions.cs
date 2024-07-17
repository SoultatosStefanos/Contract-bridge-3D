using System.Collections.Generic;
using System.Linq;
using ContractBridge.Core;

namespace Extensions
{
    public static class HandExtensions
    {
        public static IEnumerable<ICard> Grouped(this IHand hand)
        {
            return hand.OrderBy(card => card.Suit).ThenBy(card => card.Rank);
        }
    }
}