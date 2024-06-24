using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Zenject;

namespace Factories
{
    public class BoardEventDecoratorFactory : IBoardFactory
    {
        public IBoard Create()
        {
            return new BoardEventDecorator(new Board(new HandEventDecoratorFactory()));
        }
    }
}