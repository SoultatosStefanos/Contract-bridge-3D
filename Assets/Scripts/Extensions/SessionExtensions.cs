using System;
using ContractBridge.Core;

namespace Extensions
{
    public static class SessionExtensions
    {
        // NOTE: Maybe this utility should have been in the backend.
        public static IPair Pair(this ISession session, Partnership partnership)
        {
            return session.Pair(PickSeatPseudoRandomly());

            Seat PickSeatPseudoRandomly()
            {
                return partnership switch
                {
                    Partnership.NorthSouth => Seat.South,
                    Partnership.EastWest => Seat.East,
                    _ => throw new ArgumentOutOfRangeException(nameof(partnership), partnership, null)
                };
            }
        }
    }
}