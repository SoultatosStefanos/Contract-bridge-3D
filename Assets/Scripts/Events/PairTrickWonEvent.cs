using ContractBridge.Core;

namespace Events
{
    public sealed class PairTrickWonEvent
    {
        public PairTrickWonEvent(IPair pair, ITrick trick)
        {
            Pair = pair;
            Trick = trick;
        }

        public IPair Pair { get; }

        public ITrick Trick { get; }
    }
}