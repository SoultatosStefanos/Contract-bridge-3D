using Events;
using Events.Impl;
using Mappers;
using Mappers.Impl;
using Resolvers;
using Resolvers.Impl;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICardMapper>().To<CardMapper>().AsSingle();

            Container.Bind<IBoardResolver>().To<BoardResolver>().AsSingle();
            Container.Bind<IDeckResolver>().To<DeckResolver>().AsSingle();

            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }
    }
}