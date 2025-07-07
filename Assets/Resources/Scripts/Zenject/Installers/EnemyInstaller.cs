using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private EnemyLifeCycleManager _lifeCycleManager;
    public override void InstallBindings()
    {
        Container.Bind<IEnemyLifeCycleManager>().FromInstance(_lifeCycleManager).AsSingle();
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }
}