using ContractBridge.Core;
using ContractBridge.Solver;

namespace Domain
{
    public interface IAuctionExtras
    {
        IContract PickedContract { get; set; }

        IDoubleDummyContractsSolution Solution { get; set; }
    }
}