using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Zenject;

namespace Factories
{
    public class DeckEventDecoratorFactory : IDeckFactory
    {
        public IDeck Create()
        {
            return new DeckEventDecorator(new Deck(new CardFactory()));
        }
    }
}