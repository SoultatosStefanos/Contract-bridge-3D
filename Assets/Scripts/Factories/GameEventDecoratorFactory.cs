using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Events;

namespace Factories
{
    public class GameEventDecoratorFactory : IGameFactory
    {
        private readonly IEventBus _eventBus;

        public GameEventDecoratorFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IGame Create(IBoard board)
        {
            return new GameEventDecorator(new Game(board, new TrickFactory()), _eventBus);
        }
    }
}