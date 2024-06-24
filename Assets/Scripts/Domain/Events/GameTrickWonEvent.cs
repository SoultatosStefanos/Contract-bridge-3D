using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class GameTrickWonEvent
    {
        public GameTrickWonEvent(IGame game, ITrick trick, Seat seat)
        {
            Game = game;
            Trick = trick;
            Seat = seat;
        }

        public IGame Game { get; }

        public ITrick Trick { get; }

        public Seat Seat { get; }
    }
}