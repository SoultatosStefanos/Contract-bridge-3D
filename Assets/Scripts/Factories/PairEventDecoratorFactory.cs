using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;

namespace Factories
{
    public class PairEventDecoratorFactory : IPairFactory
    {
        public IPair Create(Partnership partnership)
        {
            return new PairEventDecorator(new Pair(partnership));
        }
    }
}