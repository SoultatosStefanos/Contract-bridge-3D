using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class DeckShuffleEvent
    {
        public DeckShuffleEvent(IDeck deck)
        {
            Deck = deck;
        }

        public IDeck Deck { get; }
    }
}