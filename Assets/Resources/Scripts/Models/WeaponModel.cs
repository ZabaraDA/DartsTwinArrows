using System;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }
    public Sprite _sprite;
    public Sprite Sprite 
    {
        get => _sprite;
        set
        {
            if (_sprite != value)
            {
                _sprite = value;
                OnModelSpriteChanged?.Invoke(_sprite);
                Debug.Log($"Field '{nameof(Sprite)}' changed in {typeof(WeaponModel)}");
            }
        }
    }
    public Quaternion _rotation; 
    public Quaternion Rotation 
    {
        get => _rotation;
        set
        {
            if (_rotation != value)
            {
                _rotation = value;
                OnModelRotationChanged?.Invoke(_rotation);
                Debug.Log($"Field '{nameof(Rotation)}' changed in {typeof(WeaponModel)}");
            }
        }
    }

    public WeaponModel(int id, string name, Sprite sprite, Vector2 position, Vector2 direction, IProjectileTypeModel type)
    {
        Id = id;
        Name = name;
        Sprite = sprite;
        Position = position;
        ProjectileType = type;
        Direction = direction;
    }

    public event Action<Quaternion> OnModelRotationChanged;
    public event Action<Sprite> OnModelSpriteChanged;

    public void UpdateRotation(Vector2 targetPosition)
    {
        //ToDo
    }
}
