using ContractBridge.Core;

namespace Events
{
    public sealed class AuctionTurnChangeEvent
    {
        public AuctionTurnChangeEvent(IAuction auction, Seat seat)
        {
            Auction = auction;
            Seat = seat;
        }

        public IAuction Auction { get; }

        public Seat Seat { get; }
    }
}