using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class BoardEventDecoratorFactory : IBoardFactory
    {
        public IBoard Create()
        {
            return new BoardEventDecorator(new Board(new HandEventDecoratorFactory()));
        }
    }
}