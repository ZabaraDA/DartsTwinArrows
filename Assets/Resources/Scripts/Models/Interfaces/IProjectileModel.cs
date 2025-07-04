using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    Sprite Sprite { get; }
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    IProjectileTypeModel Type { get; set; }

    void UpdatePosition(float deltaTime);
}
