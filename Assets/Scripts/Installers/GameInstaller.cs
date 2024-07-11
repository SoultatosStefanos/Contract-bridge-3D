using ContractBridge.Core;
using ContractBridge.Core.Impl;
using ContractBridge.Solver.Impl;
using Decorators;
using Domain;
using Domain.Impl;
using Events;
using Events.Impl;
using Factories;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        private readonly IAuctionExtras _auctionExtras;
        private readonly IBoard _board;

        private readonly IDeck _deck;

        private readonly IEventBus _eventBus;

        private readonly ISession _session;

        public GameInstaller()
        {
            _eventBus = new EventBus();

            _auctionExtras = new AuctionExtras(_eventBus);

            _deck = new DeckEventDecorator(new Deck(new CardFactory()), _eventBus);
            _board = new BoardEventDecorator(new Board(new HandFactory()), _eventBus);

            _session = new SessionAuctionExtrasDecorator(
                new SessionEventDecorator(
                    new Session(
                        _deck,
                        _board,
                        new PairEventDecoratorFactory(_eventBus),
                        new AuctionEventDecoratorFactory(_eventBus),
                        new GameEventDecoratorFactory(_eventBus),
                        new ScoringSystem()
                    ),
                    _eventBus
                ),
                _auctionExtras,
                new BoHaglundDoubleDummySolver(new ContractFactory()),
                _eventBus
            );
        }

        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().FromInstance(_eventBus);

            Container.Bind<IDeck>().FromInstance(_deck);
            Container.Bind<IBoard>().FromInstance(_board);
            Container.Bind<ISession>().FromInstance(_session);

            Container.Bind<IBidFactory>().To<BidFactory>().AsSingle();
            Container.Bind<IContractFactory>().To<ContractFactory>().AsSingle();

            Container.Bind<IAuctionExtras>().FromInstance(_auctionExtras);
        }
    }
}