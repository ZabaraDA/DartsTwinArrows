using UnityEngine;

public class ProjectileTypeModel : IProjectileTypeModel
{
    public int Number { get; private set; }
    public string Name { get; private set; }
    public float Speed { get; private set; }
    public Sprite Sprite { get; private set; }

    public int Damage { get; private set; }

    public ProjectileTypeModel(int id, string name, float speed, Sprite sprite, int damage)
    {
        Number = id;
        Name = name;
        Speed = speed;
        Sprite = sprite;
        Damage = damage;
    }
}
