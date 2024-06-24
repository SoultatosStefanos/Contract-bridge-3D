using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class HandClearEvent
    {
        public HandClearEvent(IHand hand)
        {
            Hand = hand;
        }

        public IHand Hand { get; }
    }
}