using UnityEngine;

public interface IProjectileTypeModel
{
    int Number { get; }
    string Name { get; }
    float Speed { get; }
    int Damage { get; }
    Sprite Sprite { get; }
}
