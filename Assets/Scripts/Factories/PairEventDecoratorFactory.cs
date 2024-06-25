using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Events;

namespace Factories
{
    public class PairEventDecoratorFactory : IPairFactory
    {
        private readonly IEventBus _eventBus;

        public PairEventDecoratorFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IPair Create(Partnership partnership)
        {
            return new PairEventDecorator(new Pair(partnership), _eventBus);
        }
    }
}