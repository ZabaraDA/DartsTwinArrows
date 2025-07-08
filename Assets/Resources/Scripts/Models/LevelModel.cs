using Newtonsoft.Json;
using UnityEngine;

public class LevelModel : ILevelModel
{
    public int Number { get; set; }
    public IWeaponTypeModel WeaponType { get; set; }
    public int EnemyCount { get; set; }
    public bool SpawnAtCenter { get; set; }
    public int SpawnCount { get; set; }
    public IEnemyTypeModel EnemyType { get; set; }
    public Vector2 WeaponSpawnPosition { get; set; }

    public LevelModel(int number, Vector2 weaponSpawnPosition, IWeaponTypeModel weaponType, IEnemyTypeModel enemyType, int enemyCount, bool spawnAtCenter, int spawnCount)
    {
        Number = number;
        WeaponSpawnPosition = weaponSpawnPosition;
        WeaponType = weaponType;
        EnemyCount = enemyCount;
        SpawnAtCenter = spawnAtCenter;
        SpawnCount = spawnCount;
        EnemyType = enemyType;
    }
}
