using ContractBridge.Core;

namespace Domain.Events
{
    public sealed class GameDoneEvent
    {
        public GameDoneEvent(IGame game)
        {
            Game = game;
        }

        public IGame Game { get; }
    }
}