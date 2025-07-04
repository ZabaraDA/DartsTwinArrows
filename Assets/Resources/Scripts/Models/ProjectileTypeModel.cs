using UnityEngine;

public class ProjectileTypeModel : IProjectileTypeModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Speed { get; private set; }
    public Sprite Sprite { get; private set; }

    public ProjectileTypeModel(int id, string name, float speed, Sprite sprite)
    {
        Id = id;
        Name = name;
        Speed = speed;
        Sprite = sprite;
    }
}
