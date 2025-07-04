using UnityEngine;

public interface IProjectileLifeCycleManager
{
    void RegisterPresenter(IProjectilePresenter presenter);
    void UnregisterPresenter(IProjectilePresenter presenter);
}
