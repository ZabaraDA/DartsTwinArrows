using System;
using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public int Id { get; set; }
    public IEnemyTypeModel Type { get; set; }
    public int CurrentHealts { get; set; }
    public Vector2 Position { get; set; }

    public event Action<int> OnModelHealtsChanged;

    public void TakeDamage(int damage)
    {
        CurrentHealts -= damage;
    }

    public EnemyModel(int id, IEnemyTypeModel type, Vector2 position)
    {
        Id = id;
        Type = type;
        Position = position;
        CurrentHealts = type.Healts;
    }

    public void UpdatePosition(float deltaTime)
    {
        //Vector2 directon = (TargetPosition - Position).normalized;
        Position += deltaTime * Type.Speed * new Vector2(0, 1);
    }
}
