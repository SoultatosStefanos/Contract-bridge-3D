using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class PairEventDecoratorFactory : IPairFactory
    {
        public IPair Create(Partnership partnership)
        {
            return new PairEventDecorator(new Pair(partnership));
        }
    }
}