using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class HandCardRemoveEvent
    {
        public HandCardRemoveEvent(IHand hand, ICard card)
        {
            Hand = hand;
            Card = card;
        }

        public IHand Hand { get; }

        public ICard Card { get; }
    }
}