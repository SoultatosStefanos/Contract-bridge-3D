using ContractBridge.Core;

namespace Domain
{
    public interface IAuctionExtras
    {
        IContract PickedContract { get; set; }
    }
}