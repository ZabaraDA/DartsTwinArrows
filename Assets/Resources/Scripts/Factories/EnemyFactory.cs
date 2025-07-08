using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private IEnemyLifeCycleManager _manager; // Ссылка на менеджер

    public EnemyFactory(IEnemyLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IEnemyPresenter Create(IEnemyModel model)
    {
        //float angle = Mathf.Atan2(model.Direction.y, model.Direction.x) * Mathf.Rad2Deg;
        //Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Enemy");
        GameObject enemy = Object.Instantiate(projectilePrefab);

        IEnemyView view = enemy.GetComponent<IEnemyView>(); // Убедитесь, что ProjectileView прикреплен к префабу

        if (view == null)
        {
            Debug.LogError("Префаб снаряда не содержит компонент ProjectileView или он не реализует IProjectileView!");
            Object.Destroy(enemy);
            return null;
        }

        // 3. Создаем Presenter, передавая ему View и Model, и менеджер жизненного цикла
        IEnemyPresenter presenter = new EnemyPresenter(view, model, _manager);
        presenter.Initialize(); // Инициализируем презентер

        return presenter;
    }
    //public IEnemyPresenter Create(int id, string name, Vector2 position, Vector2 direction, IProjectileTypeModel projectileType)
    //{
    //    IEnemyModel model = new EnemyModel(id, name, position, direction, projectileType);
    //    return Create(model);
    //}
}
