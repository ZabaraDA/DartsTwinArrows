using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    string Name { get; set; }
    Sprite Sprite { get; }
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    Transform Parent { get; set; }
    IProjectileTypeModel ProjectileType { get; set; }

    void UpdatePosition(float deltaTime);
}
