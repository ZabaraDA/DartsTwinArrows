using UnityEngine;

public class WeaponPresenter : IWeaponPresenter
{
    private IWeaponView _view;
    private IWeaponModel _model;
    private IWeaponLifeCycleManager _lifeCycleManager;
    private IProjectileFactory _projectileFactory;
    public WeaponPresenter(IWeaponView view, IWeaponModel model, IWeaponLifeCycleManager manager, IProjectileFactory projectileFactory)
    {
        _view = view;
        _model = model;
        _lifeCycleManager = manager;
        _projectileFactory = projectileFactory;
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

        _view.SetSprite(_model.Sprite);
        _view.SetRotation(_model.Rotation);
        _lifeCycleManager.RegisterPresenter(this);

        Transform projectileSpawnPosition = _view.GetProjectileSpawnPosition();

        //Vector2 newPosition = new Vector2(projectile.transform.localPosition.x + model.Id * 10f, projectile.transform.localPosition.y);

        //projectile.transform.localPosition = newPosition;

        for (int i = 0; i < _model.Type.ProjectileSpawnCount; i++)
        {
            //IProjectilePresenter projectilePresenter = _projectileFactory.Create(i, projectileSpawnPosition,_model.Direction, _model.Type.ProjectileType);
        }
    }

    public void Update(Vector2 updatableParameter)
    {
        _model.UpdateRotation(updatableParameter);
    }

    private void HandleOnModelSpriteChanged(Sprite sprite)
    {
        _view.SetSprite(sprite);
    }
    private void HandleOnModelRotationChanged(Quaternion rotation)
    {
        _view.SetRotation(rotation);
    }
}
