using ContractBridge.Core;

namespace Events
{
    public sealed class BoardDealerSetEvent
    {
        public BoardDealerSetEvent(IBoard board, Seat dealer)
        {
            Board = board;
            Dealer = dealer;
        }

        public IBoard Board { get; }

        public Seat Dealer { get; }
    }
}