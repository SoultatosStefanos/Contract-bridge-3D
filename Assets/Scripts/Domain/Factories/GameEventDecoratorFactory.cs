using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class GameEventDecoratorFactory : IGameFactory
    {
        public IGame Create(IBoard board)
        {
            return new GameEventDecorator(new Game(board, new TrickFactory()));
        }
    }
}