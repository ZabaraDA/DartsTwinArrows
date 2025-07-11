using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelPresenter : ILevelPresenter
{
    private ILevelView _view;
    private ILevelModel _model;
    private IWeaponFactory _weaponFactory;
    private IEnemyFactory _enemyFactory;

    public LevelPresenter(ILevelView view, ILevelModel model, IWeaponFactory weaponFactory, IEnemyFactory enemyFactory)
    {
        _view = view;
        _model = model;
        _weaponFactory = weaponFactory;
        _enemyFactory = enemyFactory;
    }

    public event Action<ILevelPresenter> OnPresenterLevelCompletedTriggered;

    public void Dispose()
    {
        OnPresenterLevelCompletedTriggered?.Invoke(this);
    }

    private void SpawnWeapon()
    {
        IWeaponPresenter weaponPresenter = _weaponFactory.Create(1, _model.WeaponSpawnPosition, _model.WeaponType);
    }
    private void SpawnEnemies()
    {
        Vector2 spawnPosition = new Vector2();
        for (int i = 0; i < _model.SpawnCount; i++)
        {
            IEnemyPresenter enemyPresenter = _enemyFactory.Create(i,_model.EnemyType, spawnPosition);
        }
    }

    public void Initialize()
    {
        SpawnWeapon();
        SpawnEnemies();
        Debug.Log($"LevelPresenter {_model.Number} inizialized");
    }
}
