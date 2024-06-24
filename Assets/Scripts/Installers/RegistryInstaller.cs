using Registries;
using Registries.Impl;
using Zenject;

namespace Installers
{
    public class RegistryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICardGameObjectRegistry>().To<CardGameObjectRegistry>().AsSingle();
        }
    }
}