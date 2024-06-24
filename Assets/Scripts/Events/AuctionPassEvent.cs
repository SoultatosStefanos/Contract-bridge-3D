using ContractBridge.Core;

namespace Events
{
    public sealed class AuctionPassEvent
    {
        public AuctionPassEvent(IAuction auction, Seat seat)
        {
            Auction = auction;
            Seat = seat;
        }

        public IAuction Auction { get; }

        public Seat Seat { get; }
    }
}