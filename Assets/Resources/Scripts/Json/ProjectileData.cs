using Newtonsoft.Json;

public class ProjectileData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("damage")]
    public int Damage;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
}
