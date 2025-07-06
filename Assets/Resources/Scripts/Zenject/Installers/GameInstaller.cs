using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private GameView _view;

    public override void InstallBindings()
    {
        Container.Bind<IGameView>().FromInstance(_view).AsSingle();
        Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        Container.Bind<IGamePresenter>().To<GamePresenter>().AsSingle();
    }
}