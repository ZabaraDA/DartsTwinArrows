using UnityEngine;

public interface IProjectileTypeModel
{
    int Number { get; }
    string Name { get; }
    float Speed { get; }
    Sprite Sprite { get; }
}
