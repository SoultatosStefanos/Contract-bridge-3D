using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICardManager>().To<CardManager>().AsSingle();
        Container.Bind<IBoardResolver>().To<BoardResolver>().AsSingle();
        Container.Bind<IDeckResolver>().To<DeckResolver>().AsSingle();
    }
}