using System;
using ContractBridge.Core;

namespace Extensions
{
    public static class PartnershipExtensions
    {
        public static string ToShortHandString(this Partnership partnership)
        {
            return partnership switch
            {
                Partnership.NorthSouth => "N / S",
                Partnership.EastWest => "E / W",
                _ => throw new ArgumentOutOfRangeException(nameof(partnership), partnership, null)
            };
        }
    }
}