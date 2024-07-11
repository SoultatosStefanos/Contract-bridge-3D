using ContractBridge.Solver;
using Domain;

namespace Events
{
    public class AuctionExtrasContractsSolutionEvent
    {
        public AuctionExtrasContractsSolutionEvent(IAuctionExtras auctionExtras, IDoubleDummyContractsSolution solution)
        {
            AuctionExtras = auctionExtras;
            Solution = solution;
        }

        public IAuctionExtras AuctionExtras { get; }

        public IDoubleDummyContractsSolution Solution { get; }
    }
}