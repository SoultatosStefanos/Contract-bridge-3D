using System;
using ContractBridge.Core;

namespace Extensions
{
    public static class DenominationExtensions
    {
        public static string ToShortHandString(this Denomination denomination)
        {
            return denomination switch
            {
                Denomination.Clubs => "C",
                Denomination.Diamonds => "D",
                Denomination.Hearts => "H",
                Denomination.Spades => "S",
                Denomination.NoTrumps => "NT",
                _ => throw new ArgumentOutOfRangeException(nameof(denomination), denomination, null)
            };
        }
    }
}