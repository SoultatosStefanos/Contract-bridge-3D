using ContractBridge.Core;

namespace Events
{
    public sealed class HandEmptiedEvent
    {
        public HandEmptiedEvent(IHand hand)
        {
            Hand = hand;
        }

        public IHand Hand { get; }
    }
}