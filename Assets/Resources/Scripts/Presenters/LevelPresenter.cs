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
        OnPresenterLevelCompletedTriggered?.Invoke(this, _model);
        _model.OnModelCurrentEnemyCountChanged -= HandleOnModelCurrentEnemyCountChanged;
        _weaponPresenter?.Dispose();
    }

    private void SpawnWeapon()
    {
        _weaponPresenter = _weaponFactory.Create(1, _model.WeaponSpawnPosition, _model.WeaponType);
    }
    private IEnumerator SpawnEnemies()
    {
        Vector2 topSpawnPosition1 = new Vector2(-300, 800);
        Vector2 topSpawnPosition2 = new Vector2(100, 800);
        Vector2 centerSpawnPosition = new Vector2();
        Vector2 centerSpawnPosition2 = new Vector2(-390, 0);
        string text = "JACKPOT";

        for (int i = 1; i <= _model.EnemyCount / _model.SpawnCount; i++)
        {
            Vector2 enemySpawnPosition = centerSpawnPosition;
            if (_model.EnemyType.Number == 2 || _model.EnemyType.Number == 3)
            {
                if (i % 2 == 0)
                {
                    enemySpawnPosition = topSpawnPosition1;
                }
                else
                {
                    enemySpawnPosition = topSpawnPosition2;
                }
            }
            else if (_model.EnemyType.Number == 4)
            {
                enemySpawnPosition = centerSpawnPosition2;
            }
            for (int y = 0; y < _model.SpawnCount; y++)
            {
                Vector2 spawnPosition = new Vector2(enemySpawnPosition.x + (y * 130f), enemySpawnPosition.y);
                string textPart = text[y].ToString(); 
                IEnemyPresenter enemyPresenter = _enemyFactory.Create(i*(y+1), _model.EnemyType, spawnPosition, textPart);
                enemyPresenter.OnPresenterEnemyPresenterDestoyed += HandleOnPresenterEnemyPresenterDestoyed;
                _enemyPresenters.Add(enemyPresenter);
                _statisticsPresenter.AddTotalPoints(_model.EnemyType.Healts);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void HandleOnPresenterEnemyPresenterDestoyed(IEnemyPresenter enemyPresenter)
    {
        enemyPresenter.OnPresenterEnemyPresenterDestoyed -= HandleOnPresenterEnemyPresenterDestoyed;
        _enemyPresenters.Remove(enemyPresenter);
        _model.CurrentEnemyCount -= 1;
        Debug.Log("CurrentEnemyCount: " + _model.CurrentEnemyCount);
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
