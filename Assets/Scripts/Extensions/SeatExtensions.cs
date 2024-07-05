using System;
using ContractBridge.Core;

namespace Extensions
{
    public static class SeatExtensions
    {
        public static string ToShortHandString(this Seat seat)
        {
            return seat switch
            {
                Seat.North => "N",
                Seat.East => "E",
                Seat.South => "S",
                Seat.West => "W",
                _ => throw new ArgumentOutOfRangeException(nameof(seat), seat, null)
            };
        }
    }
}