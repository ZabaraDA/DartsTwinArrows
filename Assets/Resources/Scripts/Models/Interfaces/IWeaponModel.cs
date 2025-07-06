using System;
using UnityEngine;

public interface IWeaponModel
{
    int Id { get; set; }
    string Name { get; set; }
    Sprite Sprite { get; }
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    Quaternion Rotation { get; set; }
    IProjectileTypeModel ProjectileType { get; set; }

    event Action<Quaternion> OnModelRotationChanged;
    event Action<Sprite> OnModelSpriteChanged;

    void UpdateRotation(Vector2 target);
}
