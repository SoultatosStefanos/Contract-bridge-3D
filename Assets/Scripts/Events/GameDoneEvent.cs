using ContractBridge.Core;

namespace Events
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