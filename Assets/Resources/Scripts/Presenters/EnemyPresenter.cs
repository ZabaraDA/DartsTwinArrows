using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;

    public EnemyPresenter(IEnemyView view, IEnemyModel model)
    {
        _view = view;
        _model = model;
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public void Update(float updatableParameter)
    {
        throw new System.NotImplementedException();
    }
}
