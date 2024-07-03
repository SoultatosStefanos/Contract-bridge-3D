using ContractBridge.Core;

namespace Domain.Impl
{
    public class AuctionExtras : IAuctionExtras
    {
        public IContract PickedContract { get; set; }
    }
}