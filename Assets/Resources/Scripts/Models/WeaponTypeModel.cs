using UnityEngine;

public class WeaponTypeModel : IWeaponTypeModel
{
    public int Number { get; set; }
    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }
    public int ProjectileSpawnCount { get; set; }

    public WeaponTypeModel(int number, string name, int projectileSpawnCount, Sprite sprite, IProjectileTypeModel projectileType)
    {
        Number = number;
        Name = name;
        Sprite = sprite;
        ProjectileType = projectileType;
        ProjectileSpawnCount = projectileSpawnCount;
    }
}
