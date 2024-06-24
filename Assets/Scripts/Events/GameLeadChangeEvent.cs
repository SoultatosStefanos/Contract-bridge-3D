using ContractBridge.Core;

namespace Events
{
    public sealed class GameLeadChangeEvent
    {
        public GameLeadChangeEvent(IGame game, Seat seat)
        {
            Game = game;
            Seat = seat;
        }

        public IGame Game { get; }

        public Seat Seat { get; }
    }
}