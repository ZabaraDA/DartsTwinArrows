using UnityEngine;

public interface ILifeCycleManager<T> where T : class
{
    void RegisterPresenter(T presenter);
    void UnregisterPresenter(T presenter);
}
