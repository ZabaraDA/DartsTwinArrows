using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public IProjectileTypeModel ProjectileType { get; set; }
    public Sprite Sprite => ProjectileType.Sprite;

    public Transform Parent { get; set; }

    public ProjectileModel(int id, Transform parent, Vector2 direction, IProjectileTypeModel type)
    {
        Id = id;
        ProjectileType = type;
        Parent = parent;
        Position = parent.position;
        Direction = direction;
    }

    public void UpdatePosition(float deltaTime)
    {
        Position += deltaTime * ProjectileType.Speed * Direction;
    }
}
