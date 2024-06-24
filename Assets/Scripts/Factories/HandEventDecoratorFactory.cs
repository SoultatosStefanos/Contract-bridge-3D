using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;

namespace Factories
{
    public class HandEventDecoratorFactory : IHandFactory
    {
        public IHand Create()
        {
            return new HandEventDecorator(new Hand());
        }
    }
}