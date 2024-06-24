using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Domain.Decorators;

namespace Domain.Factories
{
    public class SessionEventDecoratorFactory : ISessionFactory
    {
        public ISession Create(IDeck deck, IBoard board)
        {
            return new SessionEventDecorator(
                new Session(
                    deck,
                    board,
                    new PairEventDecoratorFactory(),
                    new AuctionEventDecoratorFactory(),
                    new GameEventDecoratorFactory(),
                    new ScoringSystem()
                )
            );
        }
    }
}