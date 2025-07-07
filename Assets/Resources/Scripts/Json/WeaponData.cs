using Newtonsoft.Json;

public class WeaponData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("projectileSpawnCount")]
    public int ProjectileSpawnCount;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
}
