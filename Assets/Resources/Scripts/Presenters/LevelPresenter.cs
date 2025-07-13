using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelPresenter : ILevelPresenter
{
    private ILevelView _view;
    private ILevelModel _model;
    private IWeaponFactory _weaponFactory;
    private IEnemyFactory _enemyFactory;
    private IStatisticsPresenter _statisticsPresenter;

    private IWeaponPresenter _weaponPresenter;
    private ICollection<IEnemyPresenter> _enemyPresenters;

    public LevelPresenter(ILevelView view, ILevelModel model, IWeaponFactory weaponFactory, IEnemyFactory enemyFactory, IStatisticsPresenter statisticsPresenter)
    {
        _view = view;
        _model = model;
        _weaponFactory = weaponFactory;
        _enemyFactory = enemyFactory;
        _statisticsPresenter = statisticsPresenter;
    }

    public event Action<ILevelPresenter, ILevelModel> OnPresenterLevelCompletedTriggered;

    public void Dispose()
    {
        _model.OnModelCurrentEnemyCountChanged -= HandleOnModelCurrentEnemyCountChanged;
        _weaponPresenter?.Dispose();
        OnPresenterLevelCompletedTriggered?.Invoke(this, _model);
    }

    private void SpawnWeapon()
    {
        _weaponPresenter = _weaponFactory.Create(1, _model.WeaponSpawnPosition, _model.WeaponType);
    }
    private IEnumerator SpawnEnemies()
    {
        Vector2 spawnPosition = new Vector2();
        for (int i = 1; i <= _model.EnemyCount / _model.SpawnCount; i++)
        {
            for (int y = 1; y <= _model.SpawnCount; y++)
            {
                IEnemyPresenter enemyPresenter = _enemyFactory.Create(i*y, _model.EnemyType, spawnPosition);
                enemyPresenter.OnPresenterEnemyPresenterDestoyed += HandleOnPresenterEnemyPresenterDestoyed;
                _enemyPresenters.Add(enemyPresenter);
                _statisticsPresenter.AddTotalPoints(_model.EnemyType.Healts);
                yield return new WaitForSeconds(2f);
            }
        }
    }

    private void HandleOnPresenterEnemyPresenterDestoyed(IEnemyPresenter enemyPresenter)
    {
        enemyPresenter.OnPresenterEnemyPresenterDestoyed -= HandleOnPresenterEnemyPresenterDestoyed;
        _enemyPresenters.Remove(enemyPresenter);
        _model.CurrentEnemyCount -= 1;

    }
    public void Initialize()
    {
        _model.OnModelCurrentEnemyCountChanged += HandleOnModelCurrentEnemyCountChanged;
        _enemyPresenters = new List<IEnemyPresenter>();
        SpawnWeapon();
        _view.StartEnemyCoroutine(SpawnEnemies());
        Debug.Log($"LevelPresenter {_model.Number} inizialized");
    }

    private void HandleOnModelCurrentEnemyCountChanged(int currentEnemyCount)
    {
        if (currentEnemyCount < 1)
        {
            Dispose();
        }
    }
}
