using ContractBridge.Core;

namespace Events
{
    public sealed class SessionPhaseChangedEvent
    {
        public SessionPhaseChangedEvent(ISession session, Phase phase)
        {
            Session = session;
            Phase = phase;
        }

        public ISession Session { get; }

        public Phase Phase { get; }
    }
}