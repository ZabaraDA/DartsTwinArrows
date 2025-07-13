using System;
using UnityEngine;

public interface ILevelModel
{
    int Number { get; set; }
    IWeaponTypeModel WeaponType { get; set; }
    IEnemyTypeModel EnemyType { get; set; }
    Vector2 WeaponSpawnPosition { get; set; }
    int EnemyCount { get; set; }
    int CurrentEnemyCount { get; set; }
    bool SpawnAtCenter { get; set; }
    int SpawnCount { get; set; }

    event Action<int> OnModelCurrentEnemyCountChanged;
}
