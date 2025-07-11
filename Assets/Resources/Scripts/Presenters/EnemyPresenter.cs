using System;
using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private IEnemyLifeCycleManager _manager;

    public event Action<IEnemyModel> OnPresenterEnemyModelDestoyed;
    public event Action<IEnemyPresenter> OnPresenterEnemyPresenterDestoyed;

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
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered += HandleOnViewTakeDamageTriggered;
        _model.OnModelHealtsChanged += HandleOnModelHealtsChanged;

        _view.SetPosition(_model.Position);
        _view.SetSprite(_model.Type.Sprite);

        _manager.RegisterPresenter(this);
    }

    public void Update(float updatableParameter)
    {
        // throw new System.NotImplementedException();
    }

    private void HandleOnModelHealtsChanged(int healts)
    {
        if (healts <= 0)
        {
            OnPresenterEnemyModelDestoyed?.Invoke(_model);
            OnPresenterEnemyPresenterDestoyed?.Invoke(this);
            Dispose();
        }
    }

    private void HandleOnViewTakeDamageTriggered(int damage)
    {
        _model.TakeDamage(damage);
    }
    private void HandleOnViewCollider2DTriggered(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            //    IProjectileView player = other.GetComponent<IProjectileView>();
            //    if (player != null)
            //    {
            //        player.TakeDamage(_model.Type.Damage);
            //    }
            //    OnPresenterEnemyModelDestoyed?.Invoke(_model);
            //    OnPresenterEnemyPresenterDestoyed?.Invoke(this);
            //    DestroyEnemy();
            //}
        }
    }
}