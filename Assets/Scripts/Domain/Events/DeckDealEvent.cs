using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class DeckDealEvent
    {
        public DeckDealEvent(IDeck deck, IBoard board)
        {
            Deck = deck;
            Board = board;
        }

        public IDeck Deck { get; }

        public IBoard Board { get; }
    }
}