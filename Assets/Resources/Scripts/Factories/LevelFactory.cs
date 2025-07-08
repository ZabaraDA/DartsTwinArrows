using UnityEngine;

public class LevelFactory : ILevelFactory
{
    private IEnemyFactory _enemyFactory;
    private IWeaponFactory _weaponFactory;

    public LevelFactory(IEnemyFactory enemyFactory, IWeaponFactory weaponFactory)
    {
        _enemyFactory = enemyFactory;
        _weaponFactory = weaponFactory;
    }

    public ILevelPresenter Create(ILevelModel model)
    {
        GameObject levelPrefab = Resources.Load<GameObject>("Prefabs/Level");
        GameObject level = Object.Instantiate(levelPrefab);

        
        if (!level.TryGetComponent<ILevelView>(out var view))
        {
            Object.Destroy(level);
            return null;
        }

        ILevelPresenter presenter = new LevelPresenter(view, model, _weaponFactory, _enemyFactory);
        presenter.Initialize();

        return presenter;
    }
}
