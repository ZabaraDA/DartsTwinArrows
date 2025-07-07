using UnityEngine;

public class LevelFactory : ILevelFactory
{
    public ILevelPresenter Create(ILevelModel model)
    {

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Level");
        GameObject projectile = Object.Instantiate(projectilePrefab);

        ILevelView view = projectile.GetComponent<ILevelView>();

        if (view == null)
        {
            Object.Destroy(projectile);
            return null;
        }

        ILevelPresenter presenter = new LevelPresenter(view, model);
        presenter.Initialize();

        return presenter;
    }
    //public ILevelPresenter Create(int id, string name, Vector2 position, Vector2 direction, IProjectileTypeModel projectileType)
    //{
    //    IProjectileModel model = new ProjectileModel(id, name, position, direction, projectileType);
    //    return Create(model);
    //}
}
