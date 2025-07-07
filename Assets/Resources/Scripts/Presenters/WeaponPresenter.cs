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
