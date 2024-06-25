using ContractBridge.Core;

namespace Events
{
    public sealed class AuctionFinalContractEvent
    {
        public AuctionFinalContractEvent(IAuction auction, IContract contract)
        {
            Auction = auction;
            Contract = contract;
        }

        public IAuction Auction { get; }

        public IContract Contract { get; }
    }
}