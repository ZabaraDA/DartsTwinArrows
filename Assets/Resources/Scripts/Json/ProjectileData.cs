using Newtonsoft.Json;

public class ProjectileData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("damage")]
    public int Damage;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
}
