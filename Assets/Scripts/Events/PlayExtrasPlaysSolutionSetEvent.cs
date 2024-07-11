using ContractBridge.Solver;
using Domain;

namespace Events
{
    public class PlayExtrasPlaysSolutionSetEvent
    {
        public PlayExtrasPlaysSolutionSetEvent(IPlayExtras playExtras, IDoubleDummyPlaysSolution solution)
        {
            PlayExtras = playExtras;
            Solution = solution;
        }

        public IPlayExtras PlayExtras { get; }

        public IDoubleDummyPlaysSolution Solution { get; }
    }
}