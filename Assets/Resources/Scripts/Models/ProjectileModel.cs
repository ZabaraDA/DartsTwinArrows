using System;
using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    private Vector2 _position;

    public event Action<Vector2> OnModelPositionChanged;
    public Vector2 Position 
    {
        get => _position;
        set
        {
            if (_position != value)
            {
                _position = value;
                OnModelPositionChanged?.Invoke(_position);
                //Debug.Log($"Field '{nameof(Rotation)}' changed in {typeof(WeaponModel)}");
            }
        }
    }
    public Vector2 Direction { get; set; }
    public IProjectileTypeModel Type { get; set; }
    public Sprite Sprite => Type.Sprite;
    public Transform Parent { get; set; }
    public bool IsMoving { get; set; }

    public ProjectileModel(int id, Transform parent, Vector2 position, IProjectileTypeModel type)
    {
        Id = id;
        Type = type;
        Parent = parent;
        Position = position;
    }

    public void UpdatePosition(float deltaTime)
    {
        Position += deltaTime * Type.Speed * Direction;
    }
}
