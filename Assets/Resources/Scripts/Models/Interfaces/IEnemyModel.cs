using System;
using UnityEngine;

public interface IEnemyModel
{
    int Id { get; set; }
    int CurrentHealts { get; set; }
    IEnemyTypeModel Type { get; set; }
    Vector2 Position { get; set; }
    //Vector2 TargetPosition { get; set; }

    event Action<Vector2> OnModelPositionChanged;
    event Action<int> OnModelHealtsChanged;

    void UpdatePosition(float deltaTime);
    void TakeDamage(int damage);
}
