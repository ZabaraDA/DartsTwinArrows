using System;
using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    string Name { get; set; }
    bool IsMoving { get; set; }
    Sprite Sprite { get; }
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    Transform Parent { get; set; }
    IProjectileTypeModel Type { get; set; }

    public event Action<Vector2> OnModelPositionChanged;

    void UpdatePosition(float deltaTime);
}
