using UnityEngine;
using Zenject.SpaceFighter;

public class ProjectilePresenter : IProjectilePresenter
{
    private IProjectileView _view;
    private IProjectileModel _model;
    private IProjectileLifeCycleManager _manager;
    public ProjectilePresenter(IProjectileView view, IProjectileModel model, IProjectileLifeCycleManager manager)
    {
        _model = model;
        _view = view;
        _manager = manager;
    }

    public void Initialize()
    {
        Debug.Log("ProjectilePresenter start Initialize");
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _model.OnModelPositionChanged += HandleOnModelPositionChanged;
        //_view.SetPosition(_model.Position);
        _view.SetSprite(_model.Sprite);

        _manager.RegisterPresenter(this);
    }

    public void Update(float deltaTime)
    {
        if (_model.IsMoving)
        {
            _model.UpdatePosition(deltaTime); // ��������� ������� � ������
        }

        // ��������� ������� � ������� ������������� �� ������ ������
        //_view.SetPosition(_model.Position);
        // ������� ������� ������ ��������� ��������� � ������������ ��� ��������
        //float currentAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
        //_view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
    }

    

    private void HandleOnModelPositionChanged(Vector2 vector)
    {
        _view.SetPosition(vector);
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
        _view.OnViewCollider2DTriggered -= HandleOnViewCollider2DTriggered;
        _model.OnModelPositionChanged -= HandleOnModelPositionChanged;
        _manager.UnregisterPresenter(this);
        if (_view as UnityEngine.Object != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());
        }
    }

    public void StartMoving()
    {
        if (!_model.IsMoving)
        {
            _view.DetachFromParent();
            _model.IsMoving = true;
        }
    }
}
