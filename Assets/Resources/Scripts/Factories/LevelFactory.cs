using UnityEngine;

public class LevelFactory : ILevelFactory
{
    private IEnemyFactory _enemyFactory;
    private IWeaponFactory _weaponFactory;
    private IStatisticsPresenter _statisticsPresenter;

    public LevelFactory(IEnemyFactory enemyFactory, IWeaponFactory weaponFactory, IStatisticsPresenter statisticsPresenter)
    {
        _enemyFactory = enemyFactory;
        _weaponFactory = weaponFactory;
        _statisticsPresenter = statisticsPresenter;
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

        ILevelPresenter presenter = new LevelPresenter(view, model, _weaponFactory, _enemyFactory,_statisticsPresenter);
        presenter.Initialize();

        return presenter;
    }
}
