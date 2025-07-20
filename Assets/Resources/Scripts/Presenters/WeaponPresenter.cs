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

        if (_view as MonoBehaviour != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());
        }

        if (GameModel.CurrentLevelNumber == 2)
        {
            Debug.Log("Destroy Point");
            var destroyPoints = GameObject.FindGameObjectsWithTag("Destroy Point");
            foreach (var destroyPoint in destroyPoints)
            {
                MonoBehaviour.Destroy(destroyPoint);
            }
        }

        var destroyProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var destroyProjectile in destroyProjectiles)
        {
            MonoBehaviour.Destroy(destroyProjectile);
        }
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
        if (GameModel.CurrentLevelNumber == 1)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GameObject destroyPointPrefab = Resources.Load<GameObject>("Prefabs/Destroy Point");
            GameObject destroyPoint = MonoBehaviour.Instantiate(destroyPointPrefab, mousePosition, Quaternion.identity);
        }
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
            projectilePresenter.OnPresenterDestroyProjectileTriggered += HandleOnPresenterDestroyProjectileTriggered;
            _waitingProjectiles.Add(projectilePresenter);
        }
    }

    private void HandleOnPresenterDestroyProjectileTriggered(IProjectilePresenter projectilePresenter)
    {
        _waitingProjectiles.Remove(projectilePresenter);
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