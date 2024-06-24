using ContractBridge.Core;
using Domain.Factories;
using Zenject;

namespace Domain.Installers
{
    public class DomainInstaller : MonoInstaller
    {
        private readonly ISession _session;

        public DomainInstaller()
        {
            var deck = new DeckEventDecoratorFactory().Create();
            var board = new BoardEventDecoratorFactory().Create();

            _session = new SessionEventDecoratorFactory().Create(deck, board);
        }

        //
        // ReSharper disable Unity.PerformanceAnalysis
        //
        public override void InstallBindings()
        {
            Container.Bind<IBoard>().FromInstance(_session.Board).AsSingle();
            Container.Bind<IDeck>().FromInstance(_session.Deck).AsSingle();
            Container.Bind<ISession>().FromInstance(_session).AsSingle();
        }
    }
}