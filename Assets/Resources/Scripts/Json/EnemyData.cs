using Newtonsoft.Json;

public class EnemyData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("healts")]
    public int Healts;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("isStatic")]
    public bool IsStatic;
    [JsonProperty("points")]
    public int Points;
    [JsonProperty("sizeMultiplier")]
    public int SizeMultiplier;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
    
}
