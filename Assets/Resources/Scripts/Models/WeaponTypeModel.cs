using UnityEngine;

public class WeaponTypeModel : IWeaponTypeModel
{
    public int Number { get; set; }
    public int ProjectileSpawnCount { get; set; }
    public float ProjectileLaunchDelay { get; set; }
    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }

    public WeaponTypeModel(int number, string name, int projectileSpawnCount, float projectileLaunchDelay, Sprite sprite, IProjectileTypeModel projectileType)
    {
        Name = name;
        Number = number;
        Sprite = sprite;
        ProjectileType = projectileType;
        ProjectileSpawnCount = projectileSpawnCount;
        ProjectileLaunchDelay = projectileLaunchDelay;
    }
}
