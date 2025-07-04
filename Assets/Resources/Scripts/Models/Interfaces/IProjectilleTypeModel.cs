using UnityEngine;

public interface IProjectileTypeModel
{
    int Id { get; }
    string Name { get; }
    float Speed { get; }
    Sprite Sprite { get; }
}
