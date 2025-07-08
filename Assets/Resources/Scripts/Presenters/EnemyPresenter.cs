using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private IEnemyLifeCycleManager _manager;

    public EnemyPresenter(IEnemyView view, IEnemyModel model, IEnemyLifeCycleManager manager)
    {
        _view = view;
        _model = model;
        _manager = manager;
    }

    public void Dispose()
    {
        _manager.UnregisterPresenter(this);
    }

    public void Initialize()
    {
        _manager.RegisterPresenter(this);
    }

    public void Update(float updatableParameter)
    {
        throw new System.NotImplementedException();
    }
}
