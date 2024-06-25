using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Events;

namespace Factories
{
    public class AuctionEventDecoratorFactory : IAuctionFactory
    {
        private readonly IEventBus _eventBus;

        public AuctionEventDecoratorFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IAuction Create()
        {
            return new AuctionEventDecorator(new Auction(new ContractFactory()), _eventBus);
        }
    }
}