using System;
using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public IEnemyTypeModel Type { get; set; }
    public int Id { get; set; }
    public int CurrentHealts { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 TargetPosition { get; set; }

    public event Action<int> OnModelHealtsChanged;

    public void TakeDamage(int damage)
    {
        CurrentHealts -= damage;
    }

    public void UpdatePosition(float deltaTime)
    {
        Vector2 directon = (TargetPosition - Position).normalized;
        Position += deltaTime * Type.Speed * directon;
    }
}
