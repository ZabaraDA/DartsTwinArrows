using UnityEngine;

public class ProjectileFactory : IProjectileFactory
{
    private IProjectileLifeCycleManager _manager; // Ссылка на менеджер

    public ProjectileFactory(IProjectileLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IProjectilePresenter Create(IProjectileModel model)
    {
        float angle = Mathf.Atan2(model.Direction.y, model.Direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        GameObject projectile = Object.Instantiate(projectilePrefab, model.Position, quaternion);



        IProjectileView view = projectile.GetComponent<IProjectileView>(); // Убедитесь, что ProjectileView прикреплен к префабу

        if (view == null)
        {
            Debug.LogError("Префаб снаряда не содержит компонент ProjectileView или он не реализует IProjectileView!");
            Object.Destroy(projectile);
            return null;
        }

        // 3. Создаем Presenter, передавая ему View и Model, и менеджер жизненного цикла
        IProjectilePresenter presenter = new ProjectilePresenter(view, model, _manager);
        presenter.Initialize(); // Инициализируем презентер

        return presenter;
    }
    public IProjectilePresenter Create(int id, string name, Vector2 position, Vector2 direction, IProjectileTypeModel projectileType)
    {
        IProjectileModel model = new ProjectileModel(id, name, position, direction, projectileType);
        return Create(model);
    }
}
