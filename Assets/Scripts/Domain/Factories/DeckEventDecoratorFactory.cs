using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class DeckEventDecoratorFactory : IDeckFactory
    {
        public IDeck Create()
        {
            return new DeckEventDecorator(new Deck(new CardFactory()));
        }
    }
}