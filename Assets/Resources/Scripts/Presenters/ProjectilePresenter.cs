using UnityEngine;
using Zenject.SpaceFighter;

public class ProjectilePresenter : IProjectilePresenter
{
    private IProjectileView _view;
    private IProjectileModel _model;
    private ProjectileLifeCycleManager _manager;
    public ProjectilePresenter(IProjectileView view, IProjectileModel model, ProjectileLifeCycleManager manager)
    {
        _model = model;
        _view = view;
        _manager = manager;
    }

    public void Initialize()
    {
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.SetPosition(_model.Position);

        _view.SetSprite(_model.Sprite);

        _manager.RegisterPresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // Обновляем позицию в модели

        // Обновляем позицию и поворот представления на основе модели
        _view.SetPosition(_model.Position);
        // Поворот снаряда должен постоянно совпадать с направлением его движения
        float currentAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
        _view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
    }

    private void DestroyEnemy()
    {
        _view.OnViewCollider2DTriggered -= HandleOnViewCollider2DTriggered;
        _manager.UnregisterPresenter(this);
        if (_view.GetGameObject() != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());
        }
    }
    private void HandleOnViewCollider2DTriggered(Collider2D other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    if (other.TryGetComponent<IEnemyView>(out var enemy))
        //    {
        //        enemy.TakeDamage(_model.Type.Damage);
        //    }
        //    DestroyEnemy();
        //}
    }

    public void Dispose()
    {
        DestroyEnemy();
    }
}
