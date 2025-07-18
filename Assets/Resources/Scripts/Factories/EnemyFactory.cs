using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private IEnemyLifeCycleManager _manager; // —сылка на менеджер
    private IStatisticsPresenter _statisticsPresenter;
    public EnemyFactory(IEnemyLifeCycleManager manager, IStatisticsPresenter statisticsPresenter)
    {
        _manager = manager;
        _statisticsPresenter = statisticsPresenter;
    }

    public IEnemyPresenter Create(IEnemyModel model)
    {

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        GameObject enemy = Object.Instantiate(projectilePrefab, model.Position, Quaternion.identity);

        Debug.Log(enemy.transform.localScale.ToString());
        Debug.Log("SizeMultiplier: " + model.Type.SizeMultiplier.ToString());
        enemy.transform.localScale = enemy.transform.localScale * model.Type.SizeMultiplier;

        if (!enemy.TryGetComponent<IEnemyView>(out var view))
        {
            Object.Destroy(enemy);
            return null;
        }

        IEnemyPresenter presenter = new EnemyPresenter(view, model, _manager, _statisticsPresenter);
        presenter.Initialize(); // »нициализируем презентер                                                                                                                                                 

        return presenter;
    }

    public IEnemyPresenter Create(int id, IEnemyTypeModel projectileType, Vector2 position, string text = null)
    {
        IEnemyModel model = new EnemyModel(id, projectileType, position, text);
        return Create(model);
    }
}
