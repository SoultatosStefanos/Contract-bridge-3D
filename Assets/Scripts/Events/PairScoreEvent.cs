using ContractBridge.Core;

namespace Events
{
    public sealed class PairScoreEvent
    {
        public PairScoreEvent(IPair pair, int score)
        {
            Pair = pair;
            Score = score;
        }

        public IPair Pair { get; }

        public int Score { get; }
    }
}