using UnityEngine;
using Zenject;

public class ProjectileInstaller : MonoInstaller
{
    [SerializeField]
    private ProjectileLifeCycleManager _lifeCycleManager;
    public override void InstallBindings()
    {
        //Container.BindInterfacesTo<IGameView>().FromInstance(_gameView).AsSingle();
        //Container.Bind<IProjectileModel>().To<ProjectileModel>().AsTransient();
        //Container.Bind<IProjectilePresenter>().To<ProjectilePresenter>().AsSingle();
        //Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
        Container.Bind<IProjectileLifeCycleManager>().FromInstance(_lifeCycleManager).AsSingle();
        Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
    }
}