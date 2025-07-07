using UnityEngine;

public class EnemyTypeModel : IEnemyTypeModel
{
   public int Number { get; set; }
    public int Healts { get; set; }
    public int Speed { get; set; }
    public bool IsStatic { get; set; }
    public int Points { get; set; }
    public Sprite Sprite { get; set; }
    public EnemyTypeModel(int number, int healts, int speed, bool isStatic, int points, Sprite sprite)
    {
        Number = number;
        Healts = healts;
        Speed = speed;
        IsStatic = isStatic;
        Points = points;
        Sprite = sprite;
    }
}
