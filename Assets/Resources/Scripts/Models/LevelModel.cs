using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelModel : ILevelModel
{
    public int Number { get; set; }
    public IWeaponTypeModel WeaponType { get; set; }
    public int EnemyCount { get; set; }
    public bool SpawnAtCenter { get; set; }
    public int SpawnCount { get; set; }
    public IEnemyTypeModel EnemyType { get; set; }
    public Vector2 WeaponSpawnPosition { get; set; }

    public int _currentEnemyCount;
    public int CurrentEnemyCount 
    {
        get => _currentEnemyCount;
        set
        {
            if (_currentEnemyCount != value)
            {
                _currentEnemyCount = value;
                OnModelCurrentEnemyCountChanged?.Invoke(_currentEnemyCount);
                //Debug.Log($"Field '{nameof(Rotation)}' changed in {typeof(WeaponModel)}");
            }
        }
    }

    public event Action<int> OnModelCurrentEnemyCountChanged;

    public LevelModel(int number, Vector2 weaponSpawnPosition, IWeaponTypeModel weaponType, IEnemyTypeModel enemyType, int enemyCount, bool spawnAtCenter, int spawnCount)
    {
        Number = number;
        WeaponSpawnPosition = weaponSpawnPosition;
        WeaponType = weaponType;
        EnemyCount = enemyCount;
        SpawnAtCenter = spawnAtCenter;
        SpawnCount = spawnCount;
        EnemyType = enemyType;
        CurrentEnemyCount = enemyCount;
    }
}
