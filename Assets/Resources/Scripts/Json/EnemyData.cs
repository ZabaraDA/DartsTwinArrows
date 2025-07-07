using Newtonsoft.Json;

public class EnemyData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("healts")]
    public int Healts;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("isStatic")]
    public bool IsStatic;
    [JsonProperty("points")]
    public int Points;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
}
