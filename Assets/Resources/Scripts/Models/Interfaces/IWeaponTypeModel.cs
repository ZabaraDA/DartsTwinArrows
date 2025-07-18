using UnityEngine;

public interface IWeaponTypeModel
{
    int Number { get; set; }
    int ProjectileSpawnCount { get; set; }
    float ProjectileLaunchDelay { get; set; }
    string Name { get; set; }
    Sprite Sprite { get; set; }
    IProjectileTypeModel ProjectileType { get; set; }
}
