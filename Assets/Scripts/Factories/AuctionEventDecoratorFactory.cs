using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;

namespace Factories
{
    public class AuctionEventDecoratorFactory : IAuctionFactory
    {
        public IAuction Create()
        {
            return new AuctionEventDecorator(new Auction(new ContractFactory()));
        }
    }
}