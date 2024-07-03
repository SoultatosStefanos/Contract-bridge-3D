using ContractBridge.Solver;
using Domain;

namespace Events
{
    public class AuctionExtrasSolutionSetEvent
    {
        public AuctionExtrasSolutionSetEvent(IAuctionExtras auctionExtras, IDoubleDummySolution solution)
        {
            AuctionExtras = auctionExtras;
            Solution = solution;
        }

        public IAuctionExtras AuctionExtras { get; }

        public IDoubleDummySolution Solution { get; }
    }
}