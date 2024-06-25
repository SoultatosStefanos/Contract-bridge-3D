using ContractBridge.Core;

namespace Events
{
    public sealed class AuctionCallEvent
    {
        public AuctionCallEvent(IAuction auction, IBid bid, Seat seat)
        {
            Auction = auction;
            Bid = bid;
            Seat = seat;
        }

        public IAuction Auction { get; }

        public IBid Bid { get; }

        public Seat Seat { get; }
    }
}