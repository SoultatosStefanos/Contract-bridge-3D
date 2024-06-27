namespace Wrappers
{
    public class AuctionDoubleWrapper : AuctionActionWrapper
    {
        public override bool CanPlayAction()
        {
            return Auction.CanDouble(PlayerSeat);
        }
    }
}