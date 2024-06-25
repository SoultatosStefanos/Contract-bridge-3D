using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Events;

namespace Factories
{
    public class HandEventDecoratorFactory : IHandFactory
    {
        private readonly IEventBus _eventBus;

        public HandEventDecoratorFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IHand Create()
        {
            return new HandEventDecorator(new Hand(), _eventBus);
        }
    }
}