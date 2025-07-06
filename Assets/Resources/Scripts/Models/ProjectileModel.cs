using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }
    public Sprite Sprite => ProjectileType.Sprite;

    public ProjectileModel(int id, string name,  Vector2 position, Vector2 direction, IProjectileTypeModel type)
    {
        Id = id;
        Name = name;
        ProjectileType = type;
        Position = position;
        Direction = direction;
    }

    public void UpdatePosition(float deltaTime)
    {
        Position += deltaTime * ProjectileType.Speed * Direction;
    }
}
