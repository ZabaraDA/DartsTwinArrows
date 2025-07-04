using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private GameView _gameView;
    public override void InstallBindings()
    {
        //Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle().NonLazy();

        // Связываем View
        Container.BindInterfacesTo<IGameView>().FromInstance(_gameView).AsSingle();
        Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        Container.Bind<IGamePresenter>().To<GamePresenter>().AsSingle();
    }
}