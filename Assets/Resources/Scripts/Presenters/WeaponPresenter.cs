using System.Collections;
using System.Collections.Generic;
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

        _view.StartWeaponCoroutine(PrepareProjectile());

        
    }

    public void Update(Vector2 updatableParameter)
    {
        _model.UpdateRotation(updatableParameter);
    }

    private void HandleOnViewMouseButtonClick()
    {
        LaunchProjectile();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Debug.Log("PlayerView position: " + mousePosition);

    }

    private void HandleOnModelSpriteChanged(Sprite sprite)
    {
        _view.SetSprite(sprite);
    }
    private void HandleOnModelRotationChanged(Quaternion rotation)
    {
        _view.SetRotation(rotation);
    }

    public IEnumerator PrepareProjectile()
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
            IProjectilePresenter projectilePresenter = _projectileFactory.Create(i, projectileSpawnPosition, new Vector2(0f, 1f), newPosition, _model.Type.ProjectileType);
            _waitingProjectiles.Add(projectilePresenter);
        }
        yield return new WaitForSeconds(_model.ProjectileLaunchDelay);
    }

    public void LaunchProjectile()
    {
        //for (int i = _waitingProjectiles.Count - 1; i >= 0; i--) // Идем в обратном порядке для безопасного удаления
        //{
        //    _waitingProjectiles[i].StartMoving();
        //}
        foreach (var projectilePresenter in _waitingProjectiles)
        {
            projectilePresenter.StartMoving();
        }
        _waitingProjectiles.Clear();
        _view.StartWeaponCoroutine(PrepareProjectile());
    }
}