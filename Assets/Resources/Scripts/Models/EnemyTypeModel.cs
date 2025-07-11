using UnityEngine;

public class EnemyTypeModel : IEnemyTypeModel
{
    public int Number { get; set; }
    public string Name { get; set; }
    public int Healts { get; set; }
    public int Speed { get; set; }
    public bool IsStatic { get; set; }
    public int Points { get; set; }
    public Sprite Sprite { get; set; }
    public int SizeMultiplier { get; set; }

    public EnemyTypeModel(int number, string name, int healts, int speed, bool isStatic, int points, Sprite sprite, int sizeMultiplier)
    {
        Number = number;
        Name = name;
        Healts = healts;
        Speed = speed;
        IsStatic = isStatic;
        Points = points;
        Sprite = sprite;
        SizeMultiplier = sizeMultiplier;
    }
}
