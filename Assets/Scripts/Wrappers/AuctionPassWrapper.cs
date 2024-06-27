namespace Wrappers
{
    public class AuctionPassWrapper : AuctionActionWrapper
    {
        public override bool CanPlayAction()
        {
            return Auction.CanPass(PlayerSeat);
        }
    }
}