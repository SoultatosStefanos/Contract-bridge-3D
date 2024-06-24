using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class AuctionRedoubleEvent
    {
        public AuctionRedoubleEvent(IAuction auction, Seat seat)
        {
            Auction = auction;
            Seat = seat;
        }

        public IAuction Auction { get; }

        public Seat Seat { get; }
    }
}