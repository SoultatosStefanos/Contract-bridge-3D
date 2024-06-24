using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class GameTurnChangeEvent
    {
        public GameTurnChangeEvent(IGame game, Seat seat)
        {
            Game = game;
            Seat = seat;
        }

        public IGame Game { get; }

        public Seat Seat { get; }
    }
}