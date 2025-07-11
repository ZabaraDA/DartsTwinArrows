using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileFactory : IProjectileFactory
{
    private IProjectileLifeCycleManager _manager; // —сылка на менеджер

    public ProjectileFactory(IProjectileLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IProjectilePresenter Create(IProjectileModel model)
    {
        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        GameObject projectile = Object.Instantiate(projectilePrefab, model.Position, model.Parent.rotation, model.Parent);
        //GameObject projectile = Object.Instantiate(projectilePrefab);

        projectile.transform.localPosition = new Vector2(projectile.transform.localPosition.x, 0);
        
        if (!projectile.TryGetComponent<IProjectileView>(out var view))
        {
            Object.Destroy(projectile);
            return null;
        }

        IProjectilePresenter presenter = new ProjectilePresenter(view, model, _manager);
        presenter.Initialize();

        return presenter;
    }

    public IProjectilePresenter Create(int id, Transform parent, Vector2 position, IProjectileTypeModel projectileType)
    {
        IProjectileModel model = new ProjectileModel(id, parent, position, projectileType);
        return Create(model);
    }
}
