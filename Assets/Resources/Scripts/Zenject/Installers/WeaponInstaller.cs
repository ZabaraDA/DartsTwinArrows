using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField]
    private WeaponLifeCycleManager _weaponLifeCycleManager;
    public override void InstallBindings()
    {
        Container.Bind<IWeaponLifeCycleManager>().FromInstance(_weaponLifeCycleManager).AsSingle();
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
    }
}