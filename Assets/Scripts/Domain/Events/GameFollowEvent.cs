using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class GameFollowEvent
    {
        public GameFollowEvent(IGame game, ICard card, Seat seat)
        {
            Game = game;
            Card = card;
            Seat = seat;
        }

        public IGame Game { get; }

        public ICard Card { get; }

        public Seat Seat { get; }
    }
}