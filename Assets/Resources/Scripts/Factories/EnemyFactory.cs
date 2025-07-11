using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private IEnemyLifeCycleManager _manager; // ������ �� ��������

    public EnemyFactory(IEnemyLifeCycleManager manager)
    {
        _manager = manager;
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

        IEnemyPresenter presenter = new EnemyPresenter(view, model, _manager);
        presenter.Initialize(); // �������������� ���������

        return presenter;
    }

    public IEnemyPresenter Create(int id, IEnemyTypeModel projectileType, Vector2 position)
    {
        IEnemyModel model = new EnemyModel(id, projectileType, position);
        return Create(model);
    }
}
