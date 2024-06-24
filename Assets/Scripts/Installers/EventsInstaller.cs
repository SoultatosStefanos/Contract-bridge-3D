using Events;
using Events.Impl;
using Zenject;

namespace Installers
{
    public class EventsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }
    }
}