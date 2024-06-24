using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;

namespace Factories
{
    public class GameEventDecoratorFactory : IGameFactory
    {
        public IGame Create(IBoard board)
        {
            return new GameEventDecorator(new Game(board, new TrickFactory()));
        }
    }
}