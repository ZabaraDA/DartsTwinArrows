using System;
using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public int Id { get; set; }
    public IEnemyTypeModel Type { get; set; }
    public int _currentHealts;
    public int CurrentHealts 
    {
        get => _currentHealts;
        set
        {
            if (_currentHealts != value)
            {
                _currentHealts = value;
                OnModelHealtsChanged?.Invoke(_currentHealts);
                Debug.Log($"Field '{nameof(CurrentHealts)}' changed in {typeof(EnemyModel)}");
            }
        }
    }
    private Vector2 _position;
    public Vector2 Position 
    {
        get => _position;
        set
        {
            if (_position != value)
            {
                _position = value;
                OnModelPositionChanged?.Invoke(_position);
            }
        }
    }

    public string Text { get; set; }

    public event Action<int> OnModelHealtsChanged;
    public event Action<Vector2> OnModelPositionChanged;

    public void TakeDamage(int damage)
    {
        CurrentHealts -= damage;
    }

    public EnemyModel(int id, IEnemyTypeModel type, Vector2 position, string text)
    {
        Id = id;
        Type = type;
        Position = position;
        CurrentHealts = type.Healts;
        Text = text;
    }

    public void UpdatePosition(float deltaTime)
    {
        //Vector2 directon = (TargetPosition - Position).normalized;
        Position += deltaTime * Type.Speed * new Vector2(0, -1);
    }
}
