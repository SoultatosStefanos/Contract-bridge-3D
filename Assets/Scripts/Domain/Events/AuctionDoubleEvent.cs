using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class AuctionDoubleEvent
    {
        public AuctionDoubleEvent(IAuction auction, Seat seat)
        {
            Auction = auction;
            Seat = seat;
        }

        public IAuction Auction { get; }

        public Seat Seat { get; }
    }
}