using Newtonsoft.Json;

public class WeaponData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("projectileSpawnCount")]
    public int ProjectileSpawnCount;
    [JsonProperty("projectileNumber")]
    public int ProjectileNumber;
    [JsonProperty("spriteNumber")]
    public int SpriteNumber;
    [JsonProperty("projectileLaunchDelay")]
    public float ProjectileLaunchDelay;

}
