using UnityEngine;

public interface ILevelModel
{
    int Number { get; set; }
    IWeaponTypeModel WeaponTypeModel { get; set; }
    int EnemyCount { get; set; }
    bool SpawnAtCenter { get; set; }
    int SpawnCount { get; set; }
}
