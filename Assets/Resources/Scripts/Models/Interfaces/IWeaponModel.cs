using System;
using UnityEngine;

public interface IWeaponModel
{
    int Id { get; set; }
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    Quaternion Rotation { get; set; }
    IWeaponTypeModel Type { get; set; }
    Sprite Sprite { get; }
    IProjectileTypeModel ProjectileType { get; }

    event Action<Quaternion> OnModelRotationChanged;
    event Action<Sprite> OnModelSpriteChanged;

    void UpdateRotation(Vector2 target);
}
