using ContractBridge.Core;
using ContractBridge.Core.Impl;
using Decorators;
using Factories;
using Zenject;

namespace Installers
{
    public class DomainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBoard>()
                .To<BoardEventDecorator>()
                .AsSingle()
                .WithArguments(new Board(new HandEventDecoratorFactory()));

            Container.Bind<IDeck>()
                .To<DeckEventDecorator>()
                .AsSingle()
                .WithArguments(new Deck(new CardFactory()));

            Container.Bind<ISession>()
                .To<SessionEventDecorator>()
                .AsSingle()
                .WithArguments(
                    new Session(
                        Container.Resolve<IDeck>(),
                        Container.Resolve<IBoard>(),
                        new PairEventDecoratorFactory(),
                        new AuctionEventDecoratorFactory(),
                        new GameEventDecoratorFactory(),
                        new ScoringSystem()
                    )
                );
        }
    }
}