using Newtonsoft.Json;
using UnityEngine;

public interface IEnemyTypeModel
{
    int Number { get; set; }
    string Name { get; set; }
    int Healts { get; set; }
    int Speed { get; set; }
    bool IsStatic { get; set; }
    int Points { get; set; }
    Sprite Sprite { get; set; }
}
