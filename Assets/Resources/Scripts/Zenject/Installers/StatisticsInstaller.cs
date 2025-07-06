using UnityEngine;
using Zenject;

public class StatisticsInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private StatisticsView _view;
    [SerializeField]
    private StatisticsLifeCycleManager _lifeCycleManager;
    public override void InstallBindings()
    {
        Container.Bind<IStatisticsView>().FromInstance(_view).AsSingle();
        Container.Bind<IStatisticsModel>().To<StatisticsModel>().AsSingle();
        Container.Bind<IStatisticsPresenter>().To<StatisticsPresenter>().AsSingle();
        Container.Bind<IStatisticsLifeCycleManager>().FromInstance(_lifeCycleManager).AsSingle();
    }
}