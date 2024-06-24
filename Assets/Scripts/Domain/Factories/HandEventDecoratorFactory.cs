using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class HandEventDecoratorFactory : IHandFactory
    {
        public IHand Create()
        {
            return new HandEventDecorator(new Hand());
        }
    }
}