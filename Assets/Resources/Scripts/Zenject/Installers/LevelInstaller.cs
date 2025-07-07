using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
    }
}