using UnityEngine;

public class WeaponTypeModel : IWeaponTypeModel
{
    public int Number { get; set; }
    public string Name { get; set; }
    public Sprite Sprite { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }
}
