using ContractBridge.Core.Impl;

namespace Domain.Impl
{
    public class AuctionExtras : IAuctionExtras
    {
        public Contract PickedContract { get; set; }
    }
}