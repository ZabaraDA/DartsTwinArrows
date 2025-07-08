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
        //float angle = Mathf.Atan2(model.Direction.y, model.Direction.x) * Mathf.Rad2Deg;
        //Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        GameObject projectile = Object.Instantiate(projectilePrefab, model.Parent);
        //GameObject projectile = Object.Instantiate(projectilePrefab);

        //Vector2 newPosition = new Vector2(projectile.transform.localPosition.x + model.Id * 10f, projectile.transform.localPosition.y);

        //projectile.transform.localPosition = newPosition;

        
        if (!projectile.TryGetComponent<IProjectileView>(out var view))
        {
            Object.Destroy(projectile);
            return null;
        }

        IProjectilePresenter presenter = new ProjectilePresenter(view, model, _manager);
        presenter.Initialize();

        return presenter;
    }

    public IProjectilePresenter Create(int id, Transform parent, Vector2 direction, Vector2 position, IProjectileTypeModel projectileType)
    {
        IProjectileModel model = new ProjectileModel(id, parent, direction, projectileType);
        return Create(model);
    }
}
