using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponPresenter : IWeaponPresenter
{
    private IWeaponView _view;
    private IWeaponModel _model;
    private IWeaponLifeCycleManager _lifeCycleManager;
    private IProjectileFactory _projectileFactory;

    private List<IProjectilePresenter> _waitingProjectiles;
    public WeaponPresenter(IWeaponView view, IWeaponModel model, IWeaponLifeCycleManager manager, IProjectileFactory projectileFactory)
    {
        _view = view;
        _model = model;
        _lifeCycleManager = manager;
        _projectileFactory = projectileFactory;
        _waitingProjectiles = new List<IProjectilePresenter>();
    }
    public void Dispose()
    {
        _model.OnModelRotationChanged -= HandleOnModelRotationChanged;
        _model.OnModelSpriteChanged -= HandleOnModelSpriteChanged;

        _view.OnViewMouseButtonClick -= HandleOnViewMouseButtonClick;

        _lifeCycleManager.UnregisterPresenter(this);
    }

    public void Initialize()
    {
        _model.OnModelRotationChanged += HandleOnModelRotationChanged;
        _model.OnModelSpriteChanged += HandleOnModelSpriteChanged;

        _view.OnViewMouseButtonClick += HandleOnViewMouseButtonClick;

        _view.SetSprite(_model.Sprite);
        _view.SetRotation(_model.Rotation);

        _lifeCycleManager.RegisterPresenter(this);

    }

    public void Update(Vector2 updatableParameter)
    {
        _model.UpdateRotation(updatableParameter);
        if (_model.CanFire() && !_waitingProjectiles.Any())
        {
            PrepareProjectile();
        }
    }
    private void HandleOnViewMouseButtonClick()
    {
        LaunchProjectile();
    }

    private void HandleOnModelSpriteChanged(Sprite sprite)
    {
        _view.SetSprite(sprite);
    }
    private void HandleOnModelRotationChanged(Quaternion rotation)
    {
        _view.SetRotation(rotation);
    }

    public void PrepareProjectile()
    {
        Transform projectileSpawnPosition = _view.GetProjectileSpawnPosition();

        for (int i = 1; i <= _model.Type.ProjectileSpawnCount; i++)
        {
            Vector2 newPosition = projectileSpawnPosition.position;
            if (_model.Type.ProjectileSpawnCount > 1)
            {
                if (i % 2 == 0)
                {
                    newPosition.x -= (i - 1) * 20f;
                }
                else
                {
                    newPosition.x += i * 20f;
                }
            }
            IProjectilePresenter projectilePresenter = _projectileFactory.Create(i, projectileSpawnPosition, newPosition, _model.Type.ProjectileType);
            _waitingProjectiles.Add(projectilePresenter);
        }
    }

    public void LaunchProjectile()
    {
        if (!_waitingProjectiles.Any())
        {
            return;
        }

        foreach (var projectilePresenter in _waitingProjectiles)
        {
            projectilePresenter.StartMoving();
        }
        _waitingProjectiles.Clear();
        _model.SetLastFireTime(Time.time);
    }
}