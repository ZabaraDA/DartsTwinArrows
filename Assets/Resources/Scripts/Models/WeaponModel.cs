using System;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    public int Id { get; set; }
    public float ProjectileLaunchDelay => Type.ProjectileLaunchDelay;
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public IProjectileTypeModel ProjectileType => Type.ProjectileType;
    public Sprite Sprite => Type.Sprite;

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
                //Debug.Log($"Field '{nameof(Rotation)}' changed in {typeof(WeaponModel)}");
            }
        }
    }

    public IWeaponTypeModel Type { get; set; }
    public float NextFireTime { get; set; }

    public WeaponModel(int id, Vector2 position, IWeaponTypeModel type)
    {
        Id = id;
        Position = position;
        Type = type;
    }

    public event Action<Quaternion> OnModelRotationChanged;
    public event Action<Sprite> OnModelSpriteChanged;

    public void UpdateRotation(Vector2 targetPosition)
    {
        Direction = (Position - targetPosition);
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
    public bool CanFire()
    {
        return Time.time >= NextFireTime;
    }

    public void SetLastFireTime(float currentTime)
    {
        NextFireTime = currentTime + ProjectileLaunchDelay;
    }
}
