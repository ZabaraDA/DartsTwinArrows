using System;
using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private IEnemyLifeCycleManager _manager;
    private IStatisticsPresenter _statisticsPresenter;

    public event Action<IEnemyModel> OnPresenterEnemyModelDestoyed;
    public event Action<IEnemyPresenter> OnPresenterEnemyPresenterDestoyed;

    public EnemyPresenter(IEnemyView view, IEnemyModel model, IEnemyLifeCycleManager manager, IStatisticsPresenter statisticsPresenter)
    {
        _view = view;
        _model = model;
        _manager = manager;
        _statisticsPresenter = statisticsPresenter;
    }

    public void Dispose()
    {
        _view.OnViewCollider2DTriggered -= HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered -= HandleOnViewTakeDamageTriggered;

        _model.OnModelHealtsChanged -= HandleOnModelHealtsChanged;
        _model.OnModelPositionChanged -= HandleOnModelPositionChanged;
        

        OnPresenterEnemyModelDestoyed?.Invoke(_model);
        OnPresenterEnemyPresenterDestoyed?.Invoke(this);

        _manager.UnregisterPresenter(this);

        if (_view as MonoBehaviour != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());
        }
    }

    public void Initialize()
    {
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered += HandleOnViewTakeDamageTriggered;

        _model.OnModelHealtsChanged += HandleOnModelHealtsChanged;
        _model.OnModelPositionChanged += HandleOnModelPositionChanged;

        _view.SetPosition(_model.Position);
        _view.SetSprite(_model.Type.Sprite);
        _view.SetText(_model.Text);

        if (_model.Type.Number == 1)
        {
            _view.SetColliderEnabled(false);
        }

        _manager.RegisterPresenter(this);
    }

    public void Update(float updatableParameter)
    {
        if (!_model.Type.IsStatic)
        {
            _model.UpdatePosition(updatableParameter);
        }
    }

    private void HandleOnModelPositionChanged(Vector2 position)
    {
        _view.SetPosition(position);
    }
    private void HandleOnModelHealtsChanged(int healts)
    {
        if (healts <= 0)
        {
            Dispose();
        }
    }

    private void HandleOnViewTakeDamageTriggered(int damage)
    {
        _model.TakeDamage(damage);
        if (_model.CurrentHealts == 1 && _model.Type.Number == 4)
        {
            _view.OpenCard();
        }
        _statisticsPresenter.AddPoints(damage);
    }
    private void HandleOnViewCollider2DTriggered(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finish!");
            Dispose();
        }
    }
}